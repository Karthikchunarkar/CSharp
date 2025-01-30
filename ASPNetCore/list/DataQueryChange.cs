using Newtonsoft.Json.Linq;
using Classes;

namespace list
{
    public class DataQueryChange<T>
    {
        public SubscriptionChangeType changeType;
        public String path;
        public String oldPath;
        public List<T> nativeData;
        public JObject data;
        public int index;
        public long count;
    }
}
