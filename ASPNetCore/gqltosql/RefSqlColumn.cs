using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using gqltosql.schema;
using store;

namespace gqltosql
{
    public class RefSqlColumn : ISqlColumn
    {
        private SqlAstNode _sub;
        private string _column;
        private string _field;
        private bool _absolute;

        public RefSqlColumn(SqlAstNode sub, string column, string field, bool absolute)
        {
            this._sub = sub;
            this._column = column;
            this._field = field;
            this._absolute = absolute;
        }

        public string FieldName { get { return _field; } } 

        public SqlAstNode Sub { get { return _sub; } }

        public void AddColumn(SqlTable table, SqlQueryContext ctx)
        {
            if(_sub.Embedded)
            {
                QueryReader reader = ctx.TypeReader.AddEmbedded(_field);
                SqlQueryContext prefix = ctx.SubPrefix(GetFieldName());
                SqlQueryContext sc = prefix.SubReader(reader);
                sc.AddSqlColumns(_sub);
            }
            else
            {
                string idColumn = _absolute ? _column : ctx.From + "." + _column;
                QueryReader reader = ctx.AddRefSelection(idColumn, _field);
                SqlQueryContext prefix = ctx.SubPrefix(GetFieldName());
                String join = prefix.GetTableAlias(_sub.Type.ToString());
                ctx.AddJoin(_sub.TableName, join, join + "._id = " + idColumn);
                SqlQueryContext sc = prefix.SubReader(reader);
                sc.AddSqlColumns(_sub);
            }
        }

        public override string ToString()
        {
            return _field;
        }

        public void ExtractDeepFields(IEntityManager em, IModelSchema schema, string type, List<OutObject> rows)
        {
            
        }

        public string GetFieldName()
        {
            throw new NotImplementedException();
        }

        public SqlAstNode GetSubQuery()
        {
            throw new NotImplementedException();
        }

        public void UpdateSubField(Dictionary<long, OutObject> parents, List<OutObject> all)
        {
            throw new NotImplementedException();
        }
    }
}
