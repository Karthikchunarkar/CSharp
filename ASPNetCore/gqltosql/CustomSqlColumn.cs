using Microsoft.EntityFrameworkCore;
using gqltosql.schema;
using store;

namespace gqltosql
{
    public class CustomSqlColumn : SqlColumn
    {
        private int _type;
        private long _id;
        private string _parentField;
        private string _valueField;
        private string _customFieldColumn;

        public CustomSqlColumn(int type, long id, string customFieldColumn, string parentField, string valueField,
            string column, string fieldName) : base(column, fieldName)
        {
            this._type = type;
            this._id = id;
            this._parentField = parentField;
            this._valueField = valueField;
            this._customFieldColumn = customFieldColumn;
        }

        public override void AddColumn(SqlTable table, SqlQueryContext ctx)
        {
            ctx.AddCustomFieldSelection(_type, _id, _customFieldColumn, _parentField, _valueField, ctx.From + "." + Column, GetFieldName());
        }

        public override void ExtractDeepFields(IEntityManager em, IModelSchema schema, string type, List<OutObject> rows)
        {
            foreach(OutObject row  in rows)
            {
                object val = row.Get(_customFieldColumn);
                row.Remove(_customFieldColumn);
                OutObjectList list = (OutObjectList)row.Get(_parentField);
                foreach(OutObject item in list)
                {
                    if(item.Id == _id)
                    {
                        item.Add("field", val);
                        break;
                    }
                }

            }
        }
    }
}
