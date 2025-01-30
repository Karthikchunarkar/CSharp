using javax.xml.crypto;
using Microsoft.AspNetCore.Http;

namespace d3e.core
{
    public class FileHelper
    {
        private static FileHelper INS;

        private D3ETempResourceHandler _tempHandler;

        public static FileHelper Get()
        {
            return INS;
        }

        public void Init()
        {
            INS = this;
        }

        public DFile CreateTempFile(String fullNameOrExtn, bool extnGiven, String content)
        {
            string fileName = null;
            string extn = null;
            if (extnGiven)
            {
                if (String.IsNullOrEmpty(fullNameOrExtn))
                {
                    return null;
                }
                extn = "." + fullNameOrExtn;
            }
            else
            {
                fileName = FileUtils.GetFileNameWithOutExtension(fullNameOrExtn);
                extn = FileUtils.GetFileExtension(fullNameOrExtn);
            }
            if (fileName == null)
            {
                fileName = "tmp";
            }
            if (String.IsNullOrEmpty(extn))
            {
                return null;
            }

            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(),
                $"{fileName}_{Path.GetRandomFileName()}{extn}");

                // Write content to file
                using (FileStream fileStream = File.Create(tempPath))
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.Write(content);
                }

                return _tempHandler.Save(new FileInfo(tempPath), fileName + extn);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public FileInfo GetStoredFile(DFile file)
        {
            try
            {
                return _tempHandler.GetFile(file.Id);

            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }

        public DFile GetTempFile(string fileName, FileStream content)
        {
            return _tempHandler.Save(fileName, content);
        }

        public DFile getTempFile(String fileName, byte[] content)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), fileName);

            // Write the byte array to the file
            File.WriteAllBytes(tempFilePath, content);

            // Open a FileStream for the created file
            FileStream fileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

            return _tempHandler.Save(fileName, fileStream);
        }
    }
}
