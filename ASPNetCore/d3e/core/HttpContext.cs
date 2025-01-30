using Microsoft.AspNetCore.Http;

namespace d3e.core
{
    public class HttpContext
    {
        public HttpRequest? request { get; }

        public HttpResponse? response {  get;}
    }
}
