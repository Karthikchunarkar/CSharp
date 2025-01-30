
using javax.xml.crypto;
using d3e.core;
using gqltosql.schema;

namespace store
{
    public class D3EPrimaryCache
    {
        private readonly Dictionary<int, Dictionary<long, DatabaseObject>> _data = new();
        private readonly Dictionary<string, DFile> _files = new();
        private readonly string _repo;
        private readonly IModelSchema _schema;

        public D3EPrimaryCache(string repo, IModelSchema schema)
        {
            _repo = repo;
            _schema = schema;
        }

        public DatabaseObject Get(int type, long id)
        {
            Dictionary<long, DatabaseObject> bytype = _data[type];
            if (bytype == null) { return null; }
            return bytype[id];
        }

        public void Add(DatabaseObject ins, int type)
        {
            Dictionary<long, DatabaseObject> byType = _data[type];
            if (byType == null)
            {
                byType = new Dictionary<long, DatabaseObject>();
                _data[type] =  byType;
            }
            byType[ins.Id] = ins;
        }

        public DatabaseObject GetOrCreate(int type, long id)
        {
            if (id == 0)
            {
                return null;
            }
            long cid = _schema.CreateCompoundId(type, id);
            var obj = Get(type, cid);
            if (obj == null)
            {
                var ins = _schema.CreateNewInstance(type, cid);
                Add(ins, type);
                return ins;
            }
            return obj;
        }

        public DFile GetOrCreateDFile(string id)
        {
            if (id == null)
            {
                return null;
            }
            DFile file = _files[id];
            if(file != null)
            {
                file = new DFile
                {
                    Id = id
                };
                file._MarkProxy(_repo);
                _files[id] = file;
            }
            return file;
        }
    }
}
