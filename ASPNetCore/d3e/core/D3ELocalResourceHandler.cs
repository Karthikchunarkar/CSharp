
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace d3e.core
{
    public class D3ELocalResourceHandler : D3EResourceHandler
    {
        private static string PREFIX = "local";

        protected string TargetPath;

        protected D3ETempResourceHandler TempHandler;

        protected ImageResizeService Resizer;

        public void Init()
        {
            Directory.CreateDirectory(TargetPath);
        }

        IFileInfo D3EResourceHandler.Get(string name)
        {
            string filePath = FileUtils.GetFilePath(name, PREFIX, TargetPath);
            try
            {
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    return new PhysicalFileInfo(fileInfo);
                }
                else
                {
                    throw new Exception($"File not found {name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Not Found: {filePath}");
                throw new Exception($"File not found {name}", ex);
            }
        }

        FileInfo D3EResourceHandler.GetFile(string name)
        {
            return FileUtils.GetFile(name, PREFIX,TargetPath);
        }

        bool D3EResourceHandler.IsSaved(DFile file)
        {
            if(file.Id == null || !file.Id.StartsWith(PREFIX))
            {
                return false;
            }
            return true;
        }

        void D3EResourceHandler.Persist(DFile file, IFileInfo resource)
        {
            string filePath = FileUtils.GetFilePath(file.Id, PREFIX, TargetPath);
            try
            {
                using (var fileStream = File.Create(filePath))
                {
                    resource.CreateReadStream();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        DFile D3EResourceHandler.Save(DFile file)
        {
            if(file.Id == null)
            {
                return null;
            }
            if(file.Id.StartsWith(PREFIX))
            {
                return file;
            }
            DFile newFile = SaveInternal(file);
            file.Id = newFile.Id;
            file.SetSize(newFile.GetSize());
            return file;
        }

        private DFile SaveInternal(DFile file)
        {
            try
            {
                string fileName = file.Id;
                string withOutPrefix = FileUtils.StripPrefix(fileName);
                string targetFilePath = Path.Combine(TargetPath, withOutPrefix);

                // Ensure directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath));

                File.Create(targetFilePath).Dispose();

                var inTemp = TempHandler.Get(fileName);
                string existingPath = inTemp;

                using (var fileStream = File.OpenRead(existingPath))
                {
                    var targetFileInfo = new FileInfo(targetFilePath);
                    return FileUtils.StoreFile(fileStream, targetFileInfo, file.GetName(), PREFIX + ":" + withOutPrefix);
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        DFile D3EResourceHandler.SaveImage(DFile file, List<ImageDimension> resizes)
        {
            foreach(var resize in resizes)
            {
                this.Resizer.Resize(file.Id, resize.Width, resize.Height, this);
            }
            return Save(file);
        }

        private DFile Save(DFile file)
        {
            throw new NotImplementedException();
        }
    }
}
