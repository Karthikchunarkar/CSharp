using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using gqltosql.schema;
using store;

namespace gqltosql
{
    public class CollSqlColumn : ISqlColumn
    {
        private string _field;

        private SqlAstNode _sub;

        public CollSqlColumn(SqlAstNode sub, string field)
        {
            this._sub = sub;
            this._field = field;
        }

        public void AddColumn(SqlTable table, SqlQueryContext ctx)
        {
            throw new NotImplementedException();
        }

        public void ExtractDeepFields(IEntityManager em, IModelSchema schema, string type, List<OutObject> rows)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return _field;
        }


        public string GetFieldName()
        {
            return _field;
        }

        public SqlAstNode GetSubQuery()
        {
            return this._sub;
        }

        public void UpdateSubField(Dictionary<long, OutObject> parents, List<OutObject> all)
        {
            Dictionary<long, OutPrimitiveList> values = new Dictionary<long, OutPrimitiveList>();
            for (int i = 0; i < all.Count; i++)
            {
                OutObject obj = all[i];
                obj.Parents.ForEach(id =>
                {
                    OutPrimitiveList val = values[id];
                    if(val == null)
                    {
                        values[id] = val = new OutPrimitiveList();
                    }
                    val.Add((OutObject) obj.GetPrimitive(GetFieldName()));
                }
                );
                obj.Parents.Clear();
            }
            foreach(var e in parents)
            {
                long key = e.Key;
                OutObject outObject = e.Value;
                if (!values.TryGetValue(key, out var val))
                {
                    val = new OutPrimitiveList();
                }

                // Add the OutPrimitiveList to the OutObject
                outObject.Add(_field, val);
            }
        }
    }
}
