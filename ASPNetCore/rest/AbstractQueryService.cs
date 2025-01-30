using Newtonsoft.Json.Linq;
using d3e.core;

namespace rest
{
    public class AbstractQueryService
    {
        protected void LogErrors(JObject errors)
        {
            Log.Info("Errors : " + errors.ToString());
        }
    }
}
