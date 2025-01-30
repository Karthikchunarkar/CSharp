
using list;

namespace gqltosql.schema
{
    public class DModel<T>
    {
        public const byte NORMAL = 0x00;
        public const byte EMBEDDED = 0x01;
        public const byte EXTERNAL = 0x02;
        public const byte TRANSIENT = 0x04;
        public const byte DOCUMENT = 0x08;
        public const byte CREATABLE = 0x10;

        private int _index;
        private string _type;
        private string _table;
        private DField<T, object>[] _fields;
        private Dictionary<string, DField<T, object>> _fieldsByName = new Dictionary<string, DField<T, object>>();
        private DModel<T> _parent;
        private int _parentCount;
        private bool _entity;
        private bool _document;
        private DModelType _modelType;
        private Func<T> _ins;
        private int[] _allTypes;
        private byte _flags;
        private string _external;

        public string Type { get => _type; set => _type = value; }

        public DModel(string type, int index, int count, int parentCount, string table, DModelType modelType, byte flags, Func<T> ins)
        {
            _type = type;
            _index = index;
            _flags = flags;
            _fields = new DField<T, object>[count];
            _parentCount = parentCount;
            _table = table;
            _modelType = modelType;
            _ins = ins;
        }

        public DModel(string type, int index, int count, int parentCount, string table, DModelType modelType, Func<T> ins)
            : this(type, index, count, parentCount, table, modelType, NORMAL, ins) { }

        public DModel(string type, int index, int count, int parentCount, string table, DModelType modelType, byte flags)
            : this(type, index, count, parentCount, table, modelType, flags, null) { }

        public DModel(string type, int index, int count, int parentCount, string table, DModelType modelType)
            : this(type, index, count, parentCount, table, modelType, NORMAL, null) { }

        public string ToColumnName()
        {
            return _table + "_id";
        }

        public DModel<T> Trans()
        {
            _flags |= TRANSIENT;
            return this;
        }

        public DModel<T> External(string name)
        {
            _flags |= EXTERNAL;
            _external = name;
            return this;
        }

        public DModel<T> Emb()
        {
            _flags |= EMBEDDED;
            return this;
        }

        public DModel<T> Document()
        {
            _flags |= DOCUMENT;
            return this;
        }

        public DModel<T> Creatable()
        {
            _flags |= CREATABLE;
            return this;
        }

        public int GetIndex()
        {
            return _index;
        }

        public DField<T, object>[] GetFields()
        {
            return _fields;
        }

        public DModelType GetModelType()
        {
            return _modelType;
        }

        public void SetEntity(bool entity)
        {
            _entity = entity;
        }

        public bool CheckFlag(byte val)
        {
            return (_flags & val) != 0;
        }

        public bool IsEmbedded()
        {
            return CheckFlag(EMBEDDED);
        }

        public bool IsExternal()
        {
            return CheckFlag(EXTERNAL);
        }

        public bool IsDocument()
        {
            return CheckFlag(DOCUMENT);
        }

        public bool IsTransient()
        {
            return CheckFlag(TRANSIENT);
        }

        public bool IsCreatable()
        {
            return CheckFlag(CREATABLE);
        }

        public string GetTableName()
        {
            return _table;
        }

        public DField<T, object> GetField(string name)
        {
           DField<T, object> f = _fieldsByName[name];
            if(f != null)
            {
                return f;
            }
            if (_parent != null) 
            {
                return _parent.GetField(name);
            }
            return null;
        }

        public DField<T, object> GetField(int index)
        {
            if (index < _parentCount)
            {
                return _parent.GetField(index);
            }
            return _fields[index - _parentCount];
        }

        public bool HasField(string name)
        {
            return GetField(name) != null;
        }

        public string GetType()
        {
            return _type;
        }

        public bool HasDeclField(string name)
        {
            return _fieldsByName.ContainsKey(name);
        }

        public void SetParent(DModel<T> parent)
        {
            _parent = parent;
        }

        public DModel<T> GetParent()
        {
            return _parent;
        }

        public void AddField(DField<T, object> field)
        {
            _fields[field.Index - _parentCount] = field;
            _fieldsByName[field.Name] = field;
        }

        public int GetParentCount()
        {
            return _parentCount;
        }

        public int GetFieldsCount()
        {
            return _fields.Length + _parentCount;
        }

        public T? NewInstance()
        {
            return _ins != null ? _ins() : default;
        }

        public DPrimField<T, R> AddEnum<R>(string name, int index, string column, DModel<object> enumClss, Func<T, R> getter, Action<T, R> setter)
        {
            var df = new DPrimField<T, R>(this, index, name, column, FieldPrimitiveType.Enum, getter, setter, null);
            df.Reference = enumClss;
            AddField(df);
            return df;
        }

        public DPrimField<T, R> AddPrimitive<R>(string name, int index, string column, FieldPrimitiveType primType, Func<T, R> getter, Action<T, R> setter)
        {
            return AddPrimitive(name, index, column, primType, getter, setter, null);
        }

        public DPrimField<T, R> AddPrimitive<R>(string name, int index, string column, FieldPrimitiveType primType, Func<T, R> getter, Action<T, R> setter, Func<T, R> def)
        {
            var field = new DPrimField<T, R>(this, index, name, column, primType, getter, setter, def);
            AddField(field);
            return field;
        }

        public DRefField<T, R> AddReference<R>(string name, int index, string column, bool child, DModel<object> refModel, Func<T, R> getter, Action<T, R> setter)
        {
            var field = new DRefField<T, R>(this, index, name, column, child, refModel, getter, setter);
            AddField(field);
            return field;
        }

        public DRefField<T, R> AddEmbedded<R>(string name, int index, string column, string prefix, DModel<object> refModel, Func<T, R> getter, Action<T, R> setter)
        {
            var field = new DEmbField<T, R>(this, index, name, column, prefix, refModel, getter, setter);
            AddField(field);
            return field;
        }

        public DRefField<T, R> AddDocReference<R>(string name, int index, string column, bool child, DModel<object> refModel, Func<T, R> getter, Action<T, R> setter)
        {
            return AddDocReference(name, index, column, null, child, refModel, getter, setter);
        }

        public DRefField<T, R> AddDocReference<R>(string name, int index, string column, string idColumn, bool child, DModel<object> refModel, Func<T, R> getter, Action<T, R> setter)
        {
            var field = new DDocField<T, R>(this, index, name, column, idColumn, child, refModel, getter, setter);
            AddField(field);
            return field;
        }

        public DRefField2<T, R> AddReference<R>(string name, int index, string column, bool child, DModel<object> refModel, Func<T, R> getter, Action<T, R> setter, Func<T, TypeAndId> refGetter)
        {
            var field = new DRefField2<T, R>(this, index, name, column, child, refModel, getter, setter, refGetter);
            AddField(field);
            return field;
        }

        public DPrimCollField<T, R> AddEnumCollection<R>(string name, int index, string column, string collTable, DModel<object> enumClss, Func<T, List<R>> getter, Action<T, List<R>> setter)
        {
            var df = new DPrimCollField<T, R>(this, index, name, column, collTable, FieldPrimitiveType.Enum, getter, setter);
            df.Reference = enumClss;
            AddField(df);
            return df;
        }

        public DPrimCollField<T, R> AddPrimitiveCollection<R>(string name, int index, string column, string collTable, FieldPrimitiveType primType, Func<T, List<R>> getter, Action<T, List<R>> setter)
        {
            var field = new DPrimCollField<T, R>(this, index, name, column, collTable, primType, getter, setter);
            AddField(field);
            return field;
        }

        public DRefCollField<T, R> AddReferenceCollection<R>(string name, int index, string column, string collTable, bool child, DModel<object> refModel, Func<T, List<R>> getter, Action<T, List<R>> setter)
        {
            var field = new DRefCollField<T, R>(this, index, name, column, child, collTable, refModel, getter, setter);
            AddField(field);
            return field;
        }

        public DDocCollField<T, R> AddDocReferenceCollection<R>(string name, int index, string column, string docColumn, string collTable, bool child, DModel<object> refModel, Func<T, List<R>> getter, Action<T, List<R>> setter)
        {
            var field = new DDocCollField<T, R>(this, index, name, column, docColumn, child, collTable, refModel, getter, setter);
            AddField(field);
            return field;
        }

        public DRefCollField2<T, R> AddReferenceCollection<R>(string name, int index, string column, string collTable, bool child, DModel<object> refModel, Func<T, List<R>> getter, Action<T, List<R>> setter, Func<T, List<TypeAndId>> refGetter)
        {
            var field = new DRefCollField2<T, R>(this, index, name, column, child, collTable, refModel, getter, setter, refGetter);
            AddField(field);
            return field;
        }

        public DFlatField<T, R> AddFlatCollection<R>(string name, int index, string column, string collTable, DModel<object> refModel, Func<T, List<R>> getter, params string[] flatPaths)
        {
            var field = new DFlatField<T, R>(this, index, name, column, collTable, refModel, getter, flatPaths);
            AddField(field);
            return field;
        }

        public DInverseCollField<T, R> AddInverseCollection<R>(string name, int index, string column, DModel<object> refModel, Func<T, List<R>> getter)
        {
            var field = new DInverseCollField<T, R>(this, index, name, column, refModel, getter);
            AddField(field);
            return field;
        }

        public int[] GetAllTypes()
        {
            return _allTypes;
        }

        public void SetAllTypes(int[] allTypes)
        {
            _allTypes = allTypes;
        }

        public string GetExternal()
        {
            return _external;
        }

        public override string ToString()
        {
            return _type;
        }

        public void AddAllParents(List<int> allParents)
        {
            if (_parent != null)
            {
                allParents.Add(_parent.GetIndex());
                _parent.AddAllParents(allParents);
            }
        }

        public DModel<T> GetMostParent()
        {
            if (_parent != null)
            {
                return _parent.GetMostParent();
            }
            return this;
        }
    }
}
