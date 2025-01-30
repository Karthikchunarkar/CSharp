using System.Data;
using System.Data.Common;
using System.Text;
using d3e.core;
using gqltosql;
using gqltosql.schema;
using rest;

namespace store
{
    public class D3EEntityManagerProvider
    {
        private D3EQueryBuilder _queryBuilder;
        private IModelSchema _schema;
        private DbConnection _jdbcTemplate;

        private AsyncLocal<IEntityManager> _entityManager = new AsyncLocal<IEntityManager>();

        private string _repo;

        public D3EQueryBuilder QueryBuilder { get => _queryBuilder; set => _queryBuilder = value; }
        public IModelSchema Schema { get => _schema; set => _schema = value; }
        public DbConnection JdbcTemplate { get => _jdbcTemplate; set => _jdbcTemplate = value; }

        public D3EEntityManagerProvider(D3EQueryBuilder queryBuilder, IModelSchema schema,
            DbConnection jdbcTemplate)
        {
            this.QueryBuilder = queryBuilder;
            this.Schema = schema;
            this.JdbcTemplate = jdbcTemplate;
        }

        public bool Create(D3EPrimaryCache entity)
        {
            if (_entityManager.Value != null)
            {
                return false;
            }
            _entityManager.Value = new EntityManagerImpl(entity);
            return true;
        }

        public void Clear()
        {
            _entityManager.Value = null;
        }

        public IEntityManager Get()
        {
            var em = _entityManager.Value;
            if (em == null)
            {
                Create(null);
                em = _entityManager.Value;
            }
            return em;
        }

        private class RowDocField : RowField
        {

            public DModel<object> type;

            public RowDocField(DModel<object> type) : base(null)
            {
                this.type = type;
            }
        }

        private class RowCustomField : RowField
        {
            public ICustomFieldProcessor<object> Processor { get; }
            public long CustomFieldId { get; }

            public RowCustomField(ICustomFieldProcessor<object> processor, long customFieldId, DField<object, object> field)
                : base(field)
            {
                Processor = processor;
                CustomFieldId = customFieldId;
            }
        }

        public class RowField
        {
            public DField<object, object> field;
            public List<RowField> subFields;

            public RowField(DField<object, object> df)
            {
                this.field = df;
            }

            public RowField(DField<object, object> df, List<RowField> subFields)
            {
                this.field = df;
                this.subFields = subFields;
            }
        }

        private class SimpleObjectMapper
        {

            private D3EPrimaryCache cache;

            public SimpleObjectMapper(D3EPrimaryCache cache)
            {
                this.cache = cache;
            }
            public DatabaseObject MapRow(IDataReader rs)
            {

                int i = 1;
                long id = rs.GetInt64(i++);
                int refType = rs.GetInt32(i++);
                if (id == 0)
                {
                    return null;
                }
                object val = cache.GetOrCreate(refType, id);
                return (DatabaseObject)val;
            }
        }

        private class SingleObjectMapper
        {
            private D3EPrimaryCache _cache;
            private DModel<object> _dm;
            private List<RowField> _selectedFields;
            private bool _unproxy;

            public SingleObjectMapper(D3EPrimaryCache cache, DModel<object> dm, List<RowField> selectedFields, bool unproxy)
            {
                this._cache = cache;
                this._dm = dm;
                this._selectedFields = selectedFields;
                this._unproxy = unproxy;
            }
            public DatabaseObject MapRow(IDataReader rs, int rowNum)
            {

                int i = 1;
                long id = rs.GetInt64(i++);
                int type = rs.GetInt32(i++);
                if (id == 0)
                {
                    return null;
                }
                DatabaseObject obj = _cache.GetOrCreate(type, id);
                if (_unproxy)
                {
                    this.
                }
                ReadObject(rs, i, obj, _selectedFields);
                if (_unproxy)
                {
                    Schema.markInProxy(obj, false);
                }
                return obj;
            }

            protected int ReadObject(IDataReader rs, int i, object obj, List<RowField> fields)
            {
                foreach (RowField rf in fields)
                {
                    if (rf is RowCustomField)
                    {
                        RowCustomField cf = (RowCustomField)rf;
                        i = cf.Processor.ReadObject(rs, i, obj, cf.field, cf.CustomFieldId);
                        continue;
                    }
                    if (rf is RowDocField)
                    {
                        RowDocField rd = (RowDocField)rf;
                        String doc = rs.GetString(i++);
                        ((DatabaseObject)obj)._SetDoc(doc);
                        continue;
                    }
                    DField<object, object> df = rf.field;
                    if (rf.subFields != null)
                    { // Embedded
                        i = ReadObject(rs, i, df.GetValue(obj), rf.subFields);
                        continue;
                    }
                    FieldType type = df.GetType();
                    switch (type)
                    {
                        case FieldType.Primitive:
                            object pri = ReadPrimitive(df, rs, i);
                            df.SetValue(obj, pri);
                            i++;
                            break;
                        case FieldType.Reference:
                            DModel<object> refer = df.Reference;
                            if (refer.GetIndex() == SchemaConstants.DFile)
                            {
                                DFile file = _cache.GetOrCreateDFile(rs.GetString(i++));
                                df.SetValue(obj, file);
                            }
                            else
                            {
                                if (refer.IsDocument() && !refer.IsCreatable())
                                {
                                    DDocField<object, object> dc = (DDocField<object, object>)df;
                                    long id = rs.GetInt64(i++);
                                    int refType = rs.GetInt32(i++);
                                    string doc = rs.GetString(i++);
                                    if (id == 0)
                                    {
                                        continue;
                                    }
                                    object val = _cache.GetOrCreate(refType, id);
                                    ((DatabaseObject)val)._SetDoc(doc);
                                }
                                else
                                {
                                    long id = rs.GetInt64(i++);
                                    int refType = rs.GetInt32(i++);
                                    if (id == 0)
                                    {
                                        continue;
                                    }
                                    object val = _cache.GetOrCreate(refType, id);
                                    df.SetValue(obj, val);
                                }
                            }
                            break;
                        case FieldType.InverseCollection:
                        case FieldType.PrimitiveCollection:
                        case FieldType.ReferenceCollection:
                            break;
                        default:
                            break;

                    }
                }
                return i;
            }

            private class DFileMapper
            {


                private D3EPrimaryCache cache;
                private DFile file;

                public DFileMapper(D3EPrimaryCache cache, DFile file)
                {
                    this.cache = cache;
                    this.file = file;
                }

                public DFile MapRow(IDataReader rs, int rowNum)
                {

                    int i = 1;
                    rs.GetString(i++); // ID
                    string name = rs.GetString(i++);
                    long size = rs.GetInt64(i++);
                    string mimeType = rs.GetString(i++);
                    file.SetName(name);
                    file.SetSize(size);
                    file.SetMimeType(mimeType);
                    return file;
                }

            }

            private class CollectionMapper
            {


                private D3EPrimaryCache _cache;
                private DField<object, object> _field;

                public CollectionMapper(D3EPrimaryCache cache, DField<object, object> field)
                {
                    this._cache = cache;
                    this._field = field;
                }
                public object MapRow(IDataReader rs, int rowNum)
                {
                    switch (_field.GetType())
                    {
                        case FieldType.PrimitiveCollection:
                            return ReadPrimitive(_field, rs, 1);
                        case FieldType.InverseCollection:
                        case FieldType.ReferenceCollection:
                            if (_field.Reference.GetIndex() == SchemaConstants.DFile)
                            {
                                return _cache.GetOrCreateDFile(rs.GetString(1));
                            }
                            long id = rs.GetInt64(1);
                            int refType = rs.GetInt32(2);
                            if (id == 0)
                            {
                                return null;
                            }
                            DatabaseObject obj = _cache.GetOrCreate(refType, id);
                            if (_field.Reference.IsDocument() && !_field.
                                            Reference.IsCreatable())
                            {
                                // Handling only non creatable document collection here because
                                // creatable document collection would just be ids like normal references
                                string doc = rs.GetString(3);
                                obj._SetDoc(doc);
                            }
                            return obj;
                    }
                    return null;
                }
            }
            public object? ReadPrimitive(DField<object, object> df, IDataReader rs, int i)
            {
                FieldPrimitiveType pt = df.GetPrimitiveType();
                switch (pt)
                {
                    case FieldPrimitiveType.Boolean:
                        return rs.GetBoolean(i);
                    case FieldPrimitiveType.Date:
                        var date = rs.GetDateTime(i);
                        return date == null ? null : date;
                    case FieldPrimitiveType.DateTime:
                        TimeSpan timestamp = rs.GetDateTime(i).TimeOfDay;
                        return timestamp == null ? null : timestamp;
                    case FieldPrimitiveType.Double:
                        return rs.GetDouble(i);
                    case FieldPrimitiveType.Duration:
                        long millis = rs.GetInt64(i);
                        return TimeSpan.FromMilliseconds(millis);
                    case FieldPrimitiveType.Enum:
                        string str = rs.GetString(i);
                        DModel<object> enmType = df.Reference;
                        DField<object, object> field = enmType.GetField(str);
                        if (field == null)
                        {
                            field = enmType.GetField(0);
                        }
                        object val = field.GetValue(null);
                        return val;
                    case FieldPrimitiveType.Integer:
                        return rs.GetInt64(i);
                    case FieldPrimitiveType.String:
                        return rs.GetString(i);
                    case FieldPrimitiveType.Time:
                        var time = rs.GetDateTime(i).TimeOfDay;
                        return time == null ? null : time;
                    case FieldPrimitiveType.Geolocation:
                        string loc = rs.GetString(i);
                        if (loc == null)
                        {
                            return null;
                        }
                        String[] split = loc.Split(",");
                        return new Geolocation(double.Parse(split[1]), double.Parse(split[0]));
                    default:
                        throw new Exception("UnsupportedOperationException");
                }
            }

            public class EntityManagerImpl : IEntityManager
            {
                private readonly D3EPrimaryCache _cache;

                public EntityManagerImpl(D3EPrimaryCache cache)
                {
                    _cache = cache ?? new D3EPrimaryCache("system", Schema);
                }

                public void CreateId(DatabaseObject obj)
                {
                    var type = Schema.Get(obj);
                    if (obj.GetId() == 0)
                    {
                        long id = _jdbcTemplateion.Query<long>("SELECT nextval('_d3e_sequence')").First();
                        Log.Debug("NextSeq: " + id);
                        Schema.AssignObjectId(type, obj, id);
                    }
                    foreach (var df in type.GetFields())
                    {
                        if (df.IsChild())
                        {
                            var refModel = df.Reference;
                            if (!refModel.IsEmbedded())
                            {
                                if (df.GetType() == FieldType.Reference)
                                {
                                    var child = df.GetValue(obj) as DatabaseObject;
                                    if (child != null)
                                    {
                                        CreateId(child);
                                    }
                                }
                                else if (df.GetType() == FieldType.ReferenceCollection)
                                {
                                    var list = df.GetValue(obj) as List<object>;
                                    foreach (var o in list)
                                    {
                                        CreateId(o as DatabaseObject);
                                    }
                                }
                            }
                        }
                    }
                }

                public void PersistFile(DFile file)
                {
                    try
                    {
                        var query = QueryBuilder.GenerateCreateDFileQuery(D3EQuery.Create(), file);
                        Execute(query);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.StackTrace);
                    }
                }

                public void Persist(DatabaseObject entity)
                {
                    try
                    {
                        var dm = Schema.Get(entity);
                        if (dm.IsTransient() || dm.IsEmbedded() || dm.IsExternal())
                        {
                            return;
                        }
                        Log.Debug("Persist Object: " + entity.Id);
                        if (entity.GetSaveStatus() == DBSaveStatus.New)
                        {
                            entity.SetSaveStatus(DBSaveStatus.Saved);
                            var query = QueryBuilder.GenerateCreateQuery(D3EQuery.Create(), dm, entity);
                            Execute(query);
                        }
                        else
                        {
                            var query = QueryBuilder.GenerateUpdateQuery(D3EQuery.Create(), dm, entity);
                            Execute(query);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.StackTrace);
                    }
                }

                public void Delete(DatabaseObject entity)
                {
                    try
                    {
                        if (entity.GetSaveStatus() == DBSaveStatus.New || entity.GetSaveStatus() == DBSaveStatus.Deleted)
                        {
                            return;
                        }
                        var query = QueryBuilder.GenerateDeleteQuery(D3EQuery.Create(), Schema.Get(entity), entity);
                        Execute(query);
                        entity.SetSaveStatus(DBSaveStatus.Deleted);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.StackTrace);
                    }
                }

                public T Find<T>(int type, long id)
                {
                    DatabaseObject databaseObject = _cache.GetOrCreate(type, id);
                    return (T) databaseObject;
                }

                public T GetById<T>(int type, long id)
                {
                    var dm = Schema.GetType(type);
                    var sb = new StringBuilder();
                    sb.Append("SELECT _id");
                    bool isCreatableDoc = dm.IsDocument() && dm.IsCreatable();
                    string docColName = dm.GetTableName() + "_doc";
                    if (isCreatableDoc)
                    {
                        sb.Append(", ").Append(docColName);
                    }
                    sb.Append(" FROM ").Append(dm.GetTableName()).Append(" WHERE _id = ").Append(id);
                    string query = sb.ToString();
                    Log.Debug("By Id: type: " + type + ", id: " + id + " , " + query);
                    var list = JdbcTemplate.Query<Dictionary<string, object>>(query).ToList();
                    if (list.Count == 0)
                    {
                        return null;
                    }
                    var dbObj = _cache.GetOrCreate(dm.GetIndex(), id) as DatabaseObject;
                    if (isCreatableDoc)
                    {
                        var map = list[0];
                        var doc = map[docColName] as string;
                        dbObj._SetDoc(doc);
                    }
                    return dbObj as T;
                }

                public List<T> LoadAll<T>(int type, long size, long page)
                {
                    var dm = Schema.GetType(type);
                    if (dm.IsTransient())
                    {
                        return new List<T>();
                    }
                    var selectedFields = new List<RowField>();
                    string query = QueryBuilder.GenerateLoadAllQuery(dm, selectedFields, size * page, size);
                    var list = _jdbcTemplateion.Query<DatabaseObject>(query, new SingleObjectMapper(_cache, dm, selectedFields, true)).ToList();
                    return list.Cast<T>().ToList();
                }

                public List<T> FindAll<T>(int type)
                {
                    var dm = Schema.GetType(type);
                    var sb = new StringBuilder();
                    var joins = new List<string>();
                    var ag = new AliasGenerator();
                    sb.Append("SELECT ").Append(QueryBuilder.CreateRefColumn(dm, dm, "_id", joins, ag)).Append(" FROM ")
                        .Append(dm.GetTableName());
                    if (joins.Count > 0)
                    {
                        foreach (var j in joins)
                        {
                            sb.Append(" LEFT JOIN ").Append(j);
                        }
                    }
                    string query = sb.ToString();
                    Log.Debug("Find All: type: " + type + " , " + query);
                    var list = _jdbcTemplateion.Query<DatabaseObject>(query, new SimpleObjectMapper(_cache)).ToList();
                    return list.Cast<T>().ToList();
                }

                private void Execute(D3EQuery mainQuery)
                {
                    if (mainQuery == null)
                    {
                        return;
                    }
                    foreach (var query in mainQuery.Queries)
                    {
                        if (query.Query != null)
                        {
                            string q = query.Query;
                            var args = query.Args;
                            Log.Info("Insert/Update: " + q);
                            var argsArray = new object[args.Count];
                            for (int i = 0; i < args.Count; i++)
                            {
                                var arg = args[i];
                                if (arg is DatabaseObject dbObj)
                                {
                                    long id = Schema.GetDatabaseId(dbObj);
                                    if (id == 0)
                                    {
                                        throw new Exception("Object references an unsaved instance - save the instance before flushing");
                                    }
                                    arg = id;
                                }
                                if (arg is DFile file)
                                {
                                    string id = file.Id;
                                    if (string.IsNullOrEmpty(id))
                                    {
                                        throw new Exception("Object references an unsaved instance - save the instance before flushing");
                                    }
                                    arg = id;
                                }
                                argsArray[i] = arg;
                            }
                            _jdbcTemplateion.Execute(q, argsArray);
                        }
                        else
                        {
                            Log.Debug("Query not found to execute");
                        }
                    }
                }

                public Query CreateNativeQuery(string sql)
                {
                    return new QueryImpl(_cache, _jdbcTemplateion, sql);
                }

                public void Unproxy(DatabaseObject obj)
                {
                    var type = Schema.Get(obj);
                    bool doc = type.IsDocument();
                    long id = Schema.GetDatabaseId(obj);
                    if (doc && obj.GetDoc() != null)
                    {
                        ReadFromObjectDoc(obj, type, id);
                        return;
                    }
                    var selectedFields = new List<RowField>();
                    string query = QueryBuilder.GenerateSelectAllQuery(type, selectedFields, id);
                    var list = _jdbcTemplateion.Query<DatabaseObject>(query, new SingleObjectMapper(_cache, type, selectedFields, false)).ToList();
                    if (list.Count == 0)
                    {
                        Log.Error("Entity not found: " + type.GetTableName() + " : " + id);
                        throw new Exception("Entity not found: " + type.GetTableName() + " : " + id);
                    }
                    if (doc)
                    {
                        ReadFromObjectDoc(obj, type, id);
                    }
                }

                private void ReadFromObjectDoc(DatabaseObject obj, DModel<object> type, long id)
                {
                    JSONInputContext.FromJsonString(obj._GetDoc(), id, type.GetType(), Schema);
                }

                public void UnproxyCollection<T>(D3EPersistanceList<T> list)
                {
                    var master = list.GetMaster() as DatabaseObject;
                    var type = Schema.Get(master);
                    var field = Schema.GetCollectionField(type, list);
                    if (field.IsTransientField())
                    {
                        list.Unproxy(new List<object>());
                        return;
                    }
                    if (type.IsDocument())
                    {
                        master.CheckProxy();
                        return;
                    }
                    long id = Schema.GetDatabaseId(master);
                    string query = QueryBuilder.GenerateSelectCollectionQuery(type, field, id);
                    Log.Info("Unproxy Collection: " + master.GetId() + ", " + field.Name + " : " + query);
                    var result = _jdbcTemplateion.Query<object>(query, new CollectionMapper(_cache, field)).ToList();
                    list.Unproxy(result);
                }

                public void UnproxyDFile(DFile file)
                {
                    string query = QueryBuilder.GenerateSelectDFileQuery(file);
                    Log.Info("Unproxy DFile: " + file.GetName() + " : " + query);
                    _jdbcTemplateion.Query<DFile>(query, new DFileMapper(_cache, file)).ToList();
                }

                public List<T> GetByIds<T>(int type, List<long> ids)
                {
                    // TODO: Implement this method
                    throw new NotImplementedException();
                }

                object IEntityManager.GetCache()
                {
                    return _cache;
                }
            }
        }
    }
}
