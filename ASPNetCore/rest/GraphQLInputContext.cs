using java.nio.file;
using d3e.core;
using store;

namespace rest
{
    public abstract class GraphQLInputContext
    {
        protected EntityHelperService HelperService { get; }
        protected Dictionary<long, object> InputObjectCache { get; }
        protected Dictionary<string, DFile> Files { get; }

        public GraphQLInputContext(EntityHelperService helperService)
        {
            this.HelperService = helperService;
            this.InputObjectCache = new Dictionary<long, object>();
            this.Files = new Dictionary<string, DFile>();
        }

        public GraphQLInputContext(EntityHelperService helperService, Dictionary<long, object> inputObjectCache, Dictionary<string, DFile> files)
        {
            HelperService = helperService;
            InputObjectCache = inputObjectCache == null ? new Dictionary<long, object>() : inputObjectCache;
            Files = files == null ? new Dictionary<string, DFile>() : files;
        }

        protected abstract GraphQLInputContext CreateContext(string field);

        public long ReadLong(string field1, string field2)
        {
            GraphQLInputContext ctx = CreateContext(field1);
            if (ctx == null)
            {
                return 0;
            }
            return ctx.ReadLong(field2);
        }

        public DFile ReadDFile(string field)
        {
            var ctx = CreateContext(field);
            return ctx == null ? null : ReadDFileInternal(ctx);
        }

        protected DFile ReadDFileInternal(GraphQLInputContext ctx)
        {
            var id = ctx.ReadString("id");
            DFile entity = Files[id];
            if (entity != null)
            {
                return entity;
            }
            else
            {
                entity = new DFile();
            }
            if (ctx.Has("size"))
            {
                entity.SetSize(ctx.ReadLong("size"));
            }
            if (ctx.Has("name"))
            {
                entity.SetSize(ctx.ReadLong("name"));
            }
            if (ctx.Has("id"))
            {
                entity.Id = id;
            }
            return entity;
        }

        public T ReadObject<T>(string type, bool readFully)
        {
            if (Has("__typeName")) type = ReadString("__typeName");
            var helper = HelperService.Get(type);

            object obj;
            if (Has("id"))
                obj = ReadRef(helper, ReadLong("id"));
            else
                obj = helper.NewInstance();

            if (readFully)
            {
                // helper.FromInput(obj, this);
            }

            return obj as T;
        }

        public T ReadRef<T>(EntityHelper<T> helper, long id) where T : class
        {
            if (id > 0) return helper.GetById(id);
            if (id == 0) return null;

            if (InputObjectCache.TryGetValue(id, out var obj))
                return obj as T;

            var newObj = helper.NewInstance();
            if (newObj is DatabaseObject dbObj)
            {
                dbObj.SetLocalId(id);
                InputObjectCache[id] = dbObj;
            }
            return (T)newObj;
        }

        protected T ReadEnumInternal<T>(string name) where T : struct, Enum
        {
            return Enum.TryParse<T>(name, out var result) ? result : default;
        }

        public T ReadEnum<T>(string field) where T : struct, Enum
        {
            var name = ReadString(field);
            return name == null ? default : ReadEnumInternal<T>(name);
        }

        public T ReadInto<T>(string field, T into) where T : IGraphQLInput
        {
            var ctx = CreateContext(field);
            if (ctx == null) return default;
            into.FromInput(ctx);
            return into;
        }

        public virtual T ReadEmbedded<T>(string field, string type, T exists)
        {
            var ctx = CreateContext(field);
            if (ctx == null) return default;
            // var helper = HelperService.Get(type);
            // helper.FromInput(exists, ctx);
            return exists;
        }

        // Abstract methods
        public abstract List<long> ReadLongColl(string field);
        public abstract List<string> ReadStringColl(string field);
        public abstract List<long> ReadIntegerColl(string field);
        public abstract List<T> ReadChildColl<T>(string field, string type);
        public abstract List<T> ReadRefColl<T>(string field, string type);
        public abstract List<T> ReadUnionColl<T>(string field, string type);
        public abstract List<T> ReadEnumColl<T>(string field) where T : struct, Enum;
        public abstract List<DFile> ReadDFileColl(string field);
        public abstract T ReadRef<T>(string field, string type) where T : class;
        public abstract T ReadChild<T>(string field, string type);
        public abstract T ReadUnion<T>(string field, string type);
        public abstract long ReadLong(string field);
        public abstract string ReadString(string field);
        public abstract bool Has(string field);
        public abstract long ReadInteger(string field);
        public abstract double ReadDouble(string field);
        public abstract bool ReadBoolean(string field);
        public abstract TimeSpan ReadDuration(string field);
        public abstract DateTime ReadDateTime(string field);
        public abstract TimeOnly ReadTime(string field);
        public abstract DateOnly ReadDate(string field);
    }
}
