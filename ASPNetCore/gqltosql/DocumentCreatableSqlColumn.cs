using gqltosql.schema;
using store;

namespace gqltosql
{
    public class DocumentCreatableSqlColumn : DocumentSqlColumn
    {
        private DModel<object> _type;

        public DocumentCreatableSqlColumn(DModel<object> type, Field field, DField<object, object> df, String column) : base(field, df, column)
        {
            this._type = type;
        }

        public override string GetFieldName()
        {
            return Column;
        }

        protected override DModel<object> GetType()
        {
            return _type;
        }

        protected override void AddValueToOut(OutObject o, object value)
        {
            if(value is OutObject val)
            {
                foreach (var item in val.GetFields())
                {
                    o.Add(item.Key, item.Value);
                }
                return;
            }
            base.AddValueToOut(o, value);
        }

        protected override void PopulateDocInObj(object obj, string doc)
        {
            if(obj is DatabaseObject db)
            {
                db._SetDoc(doc);
            }
        }
    }
}
