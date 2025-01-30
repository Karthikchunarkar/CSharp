using java.util;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using gqltosql.schema;
using store;

namespace gqltosql
{
    public class RefCollSqlColumn : ISqlColumn
    {
        private SqlAstNode _sub;
        private string _field;

        public RefCollSqlColumn(SqlAstNode sub, string field)
        {
            this._sub = sub;
            this._field= field;
        }

        public SqlAstNode Sub
        {
            get { return _sub; }
            set { _sub = value; }
        }

        public string Field
        {
            get { return _field; }
            set { _field = value; }
        }
        public void AddColumn(SqlTable table, SqlQueryContext ctx)
        {
            throw new NotImplementedException();
        }

        public void ExtractDeepFields(IEntityManager em, IModelSchema schema, string type, List<OutObject> rows)
        {
            throw new NotImplementedException();
        }

        public string GetFieldName()
        {
            return _field;
        }

        public SqlAstNode GetSubQuery()
        {
            return _sub;
        }

        public void UpdateSubField(Dictionary<long, OutObject> parents, List<OutObject> all)
        {
            Dictionary<long, OutObjectList> values = new Dictionary<long, OutObjectList>();
            for(int i = 0;  i < all.Count; i++)
            {
                OutObject obj = all[i];
                obj.Parents.ForEach(id =>
                {
                    if (!values.TryGetValue(id, out var val))
                    {
                        val = new OutObjectList();
                        values[id] = val;
                    }
                    val.Add(obj);
                });
                obj.Parents.Clear();
            }

            foreach (var entry in parents)
            {
                if (!values.TryGetValue(entry.Key, out var val))
                {
                    val = new OutObjectList();
                }
                entry.Value.AddCollectionField(_field, val);
            }
        }

        public override string ToString()
        {
            return _field;
        }
    }
}
