
using Microsoft.OpenApi.Extensions;

namespace gqltosql.schema
{
    public abstract class DField<T, R>
    {
        private static byte READANDWRITE = 0x0;
        private static byte WRITEONCE = 0x1;
        private static byte READONLY = 0x2;
        private static byte LOCAL = 0x4;
        private static byte NONE = 0x8;

        private int _index;
        private string _name;
        private string _column;
        private DModel<object> _refernce;
        private string _collTable;
        private string _mappedByColumn;
        private DModel<T> _decl;
        private bool _notNull;
        private bool _transientField;
        private bool _doc;

        private byte readType = READANDWRITE;
        public DField(DModel<T> decl, int index, string name, string column)
        {
            this._decl = decl;
            this._index = index;
            this._name = name;
            this._column = column;
        }

        public int Index
        {
            get { return _index; }
        }

        public string Name
        {
            get { return this._name; }
        }

        public virtual bool IsChild()
        {
            return false;
        }

        public abstract new FieldType GetType();

        public abstract FieldPrimitiveType GetPrimitiveType();

        public string GetCollTableName(string parentTable)
        {
            return _collTable;
        }

        public string GetTypeName()
        {
            FieldType type = GetType();
            switch (type)
            {
                case FieldType.ReferenceCollection:
                case FieldType.Reference:
                case FieldType.InverseCollection:
                    return Reference.GetType();
                case FieldType.Primitive:
                case FieldType.PrimitiveCollection:
                    return GetPrimitiveType().GetDisplayName();
            }
            return "";
        }

        public bool IsCollection()
        {
            FieldType type = GetType();
            switch (type)
            {
                case FieldType.ReferenceCollection:
                case FieldType.InverseCollection:
                case FieldType.PrimitiveCollection:
                    return true;
            }
            return false;
        }

        public bool IsEnum()
        {
            FieldType type = GetType();
            switch (type)
            {
                case FieldType.Primitive:
                case FieldType.PrimitiveCollection:
                    return GetPrimitiveType() == FieldPrimitiveType.Enum;
            }
            return false;
        }

        public bool IsReference()
        {
            FieldType type = GetType();
            switch (type)
            {
                case FieldType.ReferenceCollection:
                case FieldType.Reference:
                case FieldType.InverseCollection:
                    return true;
            }
            return false;
        }

        public DModel<object> Reference {  get => _refernce; set => _refernce = value; }

        public string ColumnName 
        {
            get { return _column; }
            set { _column = value; }
        }

        public string CollTable 
        {
            get => _collTable;
            set => _collTable = value;
        }

        public DModel<T> DeclType 
        {
            get => _decl;
            set => _decl = value;
        }

        public string MappedByColumn
        {
            get => _mappedByColumn;
            set => _mappedByColumn = value;
        }

        public override string ToString()
        {
            return _name;
        }

        public bool IsNotNull()
        {
            return _notNull;
        }

        public DField<T, R> NotNull()
        {
            this._notNull = true;
            return this;
        }

        public DField<T, R> WriteOnce()
        {
            this.readType = WRITEONCE;
            return this;
        }

        public DField<T, R> ReadOnly()
        {
            this.readType = READONLY;
            return this;
        }

        public DField<T, R> MarkLocal()
        {
            this.readType = LOCAL;
            return this;
        }

        public DField<T, R> MarkNone()
        {
            this.readType = NONE;
            return this;
        }

        public bool CanSend()
        {
            return this.readType == READANDWRITE || this.readType == WRITEONCE || this.readType == READONLY;
        }

        public bool CanReceive()
        {
            return this.readType == READANDWRITE || this.readType == WRITEONCE;
        }

        public bool IsTransientField()
        {
            return _transientField;
        }

        public DField<T, R> MarkTransient()
        {
            this._transientField = true;
            return this;
        }

        public DField<T, R> Document()
        {
            this._doc = true;
            return this;
        }

        public bool IsDocField()
        {
            return _doc;
        }

        public abstract object GetValue(T _this);

        public abstract object FetchValue(T _this, IDataFetcher fetcher);

        public abstract void SetValue(T _this, R value);
    }
}
