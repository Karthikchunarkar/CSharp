

using System.Web;
using Microsoft.Extensions.FileProviders;

namespace d3e.core
{
    public class D3ETempResourceHandler : D3EResourceHandler
    {
        private static string PREFIX = "temp";

        public string Get(string name)
        {
            string filePath = GetStorePath(name);
            try
            {
                if (File.Exists(filePath))
                {
                    return filePath;
                }
                else
                {
                    throw new Exception($"File not found {name}");
                }
            }
            catch (Exception)
            {
                throw new Exception($"File not found {name}");
            }
        }

        private string GetStorePath(string name)
        {
            return FileUtils.GetFilePath(name, PREFIX, Path.GetTempPath());
        }

        IFileInfo D3EResourceHandler.Get(string name)
        {
            throw new NotImplementedException();
        }

        FileInfo D3EResourceHandler.GetFile(string name)
        {
            return FileUtils.GetFile(name, PREFIX, Path.GetTempPath());
        }

        bool D3EResourceHandler.IsSaved(DFile file)
        {
            return false;
        }

        private string NormalizeFileName(string originalName)
        {
            string fileName = HttpUtility.UrlDecode(originalName).Trim();
            if(fileName.Contains(".."))
            {
                throw new Exception("Sorry! Filename contains invalid path sequence " + fileName);
            }
            return fileName;
        }

        private DFile StoreNewFile(Stream fileStream, FileInfo targetLocation, string fileName)
        {
            string id = GetFileId(targetLocation);
            return FileUtils.StoreFile(fileStream, targetLocation, fileName, id);
        }

        private string GetFileId(FileInfo file)
        {
            return PREFIX + ":" + file.Name;
        }

        public DFile Save(FileInfo file, string fileName)
        {
            string id = GetFileId(file);
            return FileUtils.CreateDFileFromFile(file, fileName, id);
        }

        void D3EResourceHandler.Persist(DFile file, IFileInfo resource)
        {
            throw new NotImplementedException();
        }

        public DFile Save(string originalName, FileStream fileStream)
        {
            string fileName = NormalizeFileName(originalName);
            try
            {
                string tempFilePath = Path.Combine(Path.GetTempPath(),
                    $"{Guid.NewGuid()}{FileUtils.GetFileExtension(originalName)}");
                var targetLocation = new FileInfo(tempFilePath);
                return StoreNewFile(fileStream, targetLocation, fileName);
            }
            catch (IOException e)
            {
                throw new IOException($"Could not store file {fileName}. Please try again!", e);
            }
        }

        DFile D3EResourceHandler.Save(DFile file)
        {
            throw new Exception("Cannot save to temp storage.");
        }

        DFile D3EResourceHandler.SaveImage(DFile file, List<ImageDimension> resizes)
        {
            throw new Exception("Cannot save to temp storage.");
        }
    }
}
