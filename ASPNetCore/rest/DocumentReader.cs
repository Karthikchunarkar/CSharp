using Newtonsoft.Json.Linq;
using d3e.core;
using store;

namespace rest
{
    public class DocumentReader : JSONInputContext
    {
        private IdGenerator _id;

        public DocumentReader(JObject json, EntityHelperService helperService, Dictionary<long, Object> inputObjectCache,
            Dictionary<string, DFile> files, JObject variables, IdGenerator id) : base(json, helperService, inputObjectCache, files, null)
        {
            this._id = id;
        }

        protected override T ReadObject<T>(string type, bool readFully)
        {
            T obj = base.ReadObject<T>(type, readFully);
            if(obj is DatabaseObject val)
            {
                val.SetId(_id.Next());
            }
            return obj;
        }

        protected override JSONInputContext CreateReadContext(JObject json)
        {
            return new DocumentReader(json, HelperService, InputObjectCache, Files, json, _id);
        }

        public class IdGenerator
        {
            private long start;

            public IdGenerator(long start)
            {
                this.start = start;
            }

            public long Next()
            {
                return ++start;
            }
        }
    }
}
