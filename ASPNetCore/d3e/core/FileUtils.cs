using System;
using System.Text;

namespace d3e.core
{
    public class FileUtils
    {
        public static string GetFilePath(string name, string prefix, string targetPath)
        {
            FileInfo file = GetFile(name, prefix, targetPath);
            var directory = Path.GetFullPath(file.FullName);
            return directory;
        }

        public static FileInfo GetFile(string name, string prefix, string targetPath)
        {
            if (name.StartsWith(prefix))
            {
                name = name.Substring(prefix.Length + 1);

                string targetLocation = targetPath + Path.DirectorySeparatorChar + name;

                return new FileInfo(targetLocation);
            }
            else
            {
                throw new java.lang.Exception("File not found" + name);
            }
        }

        public static string DetectMimeType(FileInfo file)
        {
            try
            {
                return null;
            }
            catch (java.lang.Exception e)
            {
                return " " + e.Message;
            }
        }

        public static DFile StoreFile(Stream fileStream, FileInfo file, string name, string id)
        {
            using (var stream = file.Create())
            {
                fileStream.CopyTo(stream);
            }
            return CreateDFileFromFile(file, name, id);

        }
        public static DFile CreateDFileFromFile(FileInfo file, string name, string id)
        {
            DFile dFile = new DFile();
            dFile.SetMimeType(DetectMimeType(file));
            dFile.Id = id;
            dFile.SetName(name);
            dFile.SetSize(file.Length);
            return dFile;
        }

        public static string GetResizedName(string name, int width, int height)
        {
            StringBuilder sb = new StringBuilder(name);
            string resPart = "";
            if (width != 0 && height != 0)
            {
                resPart = "_" + width + "_" + height;
            }
            int lastDot = name.LastIndexOf('.');
            sb.Insert(lastDot, resPart);
            return sb.ToString();
        }

        public static string StripPrefix(string id)
        {
            int prefixEnd = id.IndexOf(":");
            if(prefixEnd == -1)
            {
                return id;
            }
            return id.Substring(prefixEnd + 1);
        }

        public static string GetFileExtension(string fileName)
        {
            int lastDot = fileName.LastIndexOf(".");
            if(lastDot == -1)
            {
                return ".d3e";
            }
            return fileName.Substring(lastDot);
        }

        public static string GetFileNameWithOutExtension(string fileName)
        {
            int lastDot = fileName.LastIndexOf(".");
            if (lastDot == -1)
            {
                return fileName;
            }
            return fileName.Substring(0, lastDot);
        }
    }
}
