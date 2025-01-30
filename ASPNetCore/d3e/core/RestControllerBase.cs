using Microsoft.AspNetCore.Http;
using gqltosql.schema;
using store;

namespace d3e.core
{
    public class RestControllerBase
    {
        private HttpContext _ctx;

        private D3ETempResourceHandler _saveHandler;

        private IModelSchema _schema;

        public HttpContext GetContext() { return _ctx; }

        protected void MarkNotFount()
        {
            _ctx.response.StatusCode = StatusCodes.Status404NotFound;
        }

        protected void MarkForbidden()
        {
            _ctx.response.StatusCode = StatusCodes.Status403Forbidden;
        }

        protected void MarkSeverError()
        {
            _ctx.response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        protected void MarkBadRequest()
        {
            _ctx.response.StatusCode = StatusCodes.Status400BadRequest;
        }

        protected DFile UploadFile(IFormFile file)
        {
            string fileName = file.FileName;
            try
            {
                DFile result = _saveHandler.Save(fileName, new FileStream(file.FileName,
                    FileMode.Create, FileAccess.Write, FileShare.None));

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected List<DFile> UploadFiles(IFormFile[] files)
        {
            return files.Select(v => UploadFile(v)).ToList();
        }


        protected DBObject ConvertFromJson<T>(string json, string type) where T : DBObject
        {
            return null;
        }
    }
}
