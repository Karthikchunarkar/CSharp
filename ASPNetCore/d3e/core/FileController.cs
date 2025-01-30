using System.Net.Http.Headers;
using System.Net.Mime;
using com.sun.xml.@internal.messaging.saaj.packaging.mime.internet;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace d3e.core
{
    [ApiController]
    public class FileController : ControllerBase
    {
        private static readonly int TIMEOUT = 365 * 24 * 60 * 60;


        private Dictionary<string, D3EResourceHandler> _handlers;

        private D3ETempResourceHandler _saveHandler;

        protected ImageResizeService Resizer;

        private static FileController _INIt;

        public void Init()
        {
            _INIt = this;
        }

        public static IFileInfo Load(DFile file)
        {
            D3EResourceHandler loadHandler = _INIt._handlers.GetValueOrDefault(_INIt.GetPrefix(file.Id), null);
            if (loadHandler == null)
            {
                throw new Exception("Resource not found");
            }
            IFileInfo resource = _INIt.LoadFilesAsResource(loadHandler, file.Id, 0, 0);
            return resource;
        }

        [HttpPost("/api/upload")]
        public DFile UploadFile(IFormFile formFile)
        {
            string fileName = formFile.FileName;
            try
            {
                DFile file = _saveHandler.Save(fileName, new FileStream(formFile.FileName,
                     FileMode.Create, FileAccess.Write, FileShare.None));

                return file;
            }
            catch (Exception e)
            {
                Log.PrintStackTrace(e);
                throw new Exception("Could not store file " + fileName + ". Please try again!" + e.Message);
            }
        }

        [HttpPost("/api/uploads")]
        public List<DFile> UploadMultipleFiles(IFormFile[] files)
        {
            return files.Select(f => UploadFile(f)).ToList();
        }

        [EnableCors("AllowAll")]
        [HttpGet("/api/download/{fileName}")]
        public async Task<IActionResult> Download(
            string fileName,
            [FromQuery] string originalName = null,
            [FromQuery] int? width = null,
            [FromQuery] int? height = null,
            [FromQuery] bool inline = false)
        {
            D3EResourceHandler loadHandler = _handlers.GetValueOrDefault(GetPrefix(fileName), null);
            if (loadHandler == null)
            {
                return NotFound();
            }

            IFileInfo resource = null;
            try
            {
                if (fileName.StartsWith("temp"))
                {
                    resource = LoadFilesAsResource(loadHandler, fileName, 0, 0);
                }
                else
                {
                    resource = LoadFilesAsResource(loadHandler, fileName, width ?? 0, height ?? 0);
                }
            }
            catch (Exception e)
            {
                if (width != null && height != null)
                {
                    try
                    {
                        LoadFilesAsResource(loadHandler, fileName, 0, 0);
                        Resizer.ResizeNow(fileName, width ?? 0, height ?? 0, loadHandler);
                        resource = LoadFilesAsResource(loadHandler, fileName, width ?? 0, height ?? 0);
                    }
                    catch (Exception e1)
                    {
                        return NotFound(e1);
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            string contentType = null;
            try
            {
                new FileExtensionContentTypeProvider().TryGetContentType(resource.Name, out var value);
                contentType = value ?? "application/octet-stream";
            }
            catch (Exception e2)
            {
                return NotFound(e2);
            }

            string headerFileName = String.IsNullOrEmpty(originalName) ? originalName : resource.Name;
            if (Object.Equals(inline, "true"))
            {
                Response.Headers.Append("Content-Disposition", "attachment; fileName=" + headerFileName + "");
            }
            Response.Headers.Append("Cache-Control", $"max-age={TIMEOUT}");

            return File(resource.CreateReadStream(), contentType);

        }

        private IFileInfo LoadFilesAsResource(D3EResourceHandler loadHandler, string fileName, int width, int height)
        {
            string reSizedName = FileUtils.GetResizedName(fileName, width, height);
            return loadHandler.Get(reSizedName);
        }

        private string GetPrefix(string id)
        {
            int first = id.IndexOf('.');
            if (first == -1)
            {
                return null;
            }
            return id.Substring(0, first);
        }
    }
}
