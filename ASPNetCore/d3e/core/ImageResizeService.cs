using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Concurrent;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace d3e.core
{
    public class ImageResizeService
    {
        private static long SLEEP_TIME = 1_000;

        private class ResizeJob
        {
            public string FileName {  get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public D3EResourceHandler Handler { get; set; }

            public ResizeJob(string fileName, int width, int height, D3EResourceHandler handler)
            {
                this.FileName = fileName;
                this.Width = width;
                this.Height = height;
                this.Handler = handler;
            }
        }

        private ConcurrentQueue<ResizeJob> _jobs = new ConcurrentQueue<ResizeJob>();


        public void Resize(string fileName, int width, int height, D3EResourceHandler handler)
        {
            PushJob(new ResizeJob(fileName, width, height, handler));
        }

        private void PushJob(ResizeJob resizeJob)
        {
            this._jobs.Enqueue(resizeJob);
        }

        public void ResizeNow(string fileName, int width, int height, D3EResourceHandler loadHandler)
        {
            ResizeAndSave(new ResizeJob(fileName, width, height, loadHandler));
        }

        public void runner()
        {
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        public void Run()
        {
            while (true)
            {
                ResizeJob job = PopJob();
                if (job == null)
                {
                    try
                    {
                        Thread.Sleep((int)SLEEP_TIME);
                    }
                    catch (Exception e) { Console.WriteLine(e); }
                }
                else
                {
                    if (!ResizeAndSave(job))
                    {
                        PushJob(job);
                    }
                }
            }
        }

        private ResizeJob PopJob()
        {
            _jobs.TryDequeue(out ResizeJob job);
            return job;
        }

        private bool ResizeAndSave(ResizeJob job)
        {
            IFileInfo originalResource = job.Handler.Get(job.FileName);
            FileInfo tempCopy = null;
            try
            {

                FileInfo orginialFile = new FileInfo(originalResource.PhysicalPath);

                tempCopy = new FileInfo(Path.Combine(orginialFile.DirectoryName, "copy_" + orginialFile.Name));
                File.Copy(orginialFile.FullName, tempCopy.FullName, true);

                FileInfo reSizedFile = new FileInfo(Path.Combine(orginialFile.DirectoryName, tempCopy.Name));

                //  Perform the resize operation and save to the new file
                using (var image = Image.FromFile(tempCopy.FullName))
                {
                    // Calculate dimensions while maintaining aspect ratio
                    var ratioX = (double)job.Width / image.Width;
                    var ratioY = (double)job.Height / image.Height;
                    var ratio = Math.Min(ratioX, ratioY);

                    var finalWidth = (int)(image.Width * ratio);
                    var finalHeight = (int)(image.Height * ratio);

                    using (var newImage = new Bitmap(finalWidth, finalHeight))
                    {
                        using (var graphics = Graphics.FromImage(newImage))
                        {
                            // Set the resize quality modes
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;

                            // Draw the resized image
                            graphics.DrawImage(image, 0, 0, finalWidth, finalHeight);

                            // Save the resized image
                            newImage.Save(tempCopy.FullName, image.RawFormat);
                        }
                    }
                }


                // Create a new DFile and set the MIME type and ID
                DFile dFile = new DFile();
                dFile.Id = FileUtils.GetResizedName(job.FileName, job.Width, job.Height);
                dFile.SetMimeType(FileUtils.DetectMimeType(reSizedFile));

                // Create a new IFileInfo for the resized file
                IFileInfo resizedFileInfo = new PhysicalFileInfo(reSizedFile);

                // Save the resized file using the original resource handler
                job.Handler.Persist(dFile, resizedFileInfo);

            }
            catch(Exception e)
            {
                return false;
            }
            finally
            {
                if(tempCopy != null && tempCopy.Exists)
                {
                    tempCopy.Delete();
                }
            }
            return true;
        }
    }
}
