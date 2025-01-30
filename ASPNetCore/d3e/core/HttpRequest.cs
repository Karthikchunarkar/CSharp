using System.Net;

namespace d3e.core
{
    public class HttpRequest
    {
        public static string GetBodyAsString(HttpWebRequest request) 
        {
            if("POST".Equals(request.Method, StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    using (var response = request.GetResponse())
                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                catch (Exception ex) 
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }
    }
}
