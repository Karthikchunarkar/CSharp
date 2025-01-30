using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using gqltosql.schema;
using store;

namespace gqltosql
{
    public class SqlColumn : ISqlColumn
    {
        public string Column;
        private string _field;

        public SqlColumn(string column, string field)
        {
            this.Column = column;
            this._field = field;
        }

        public virtual void AddColumn(SqlTable table, SqlQueryContext ctx)
        {
            ctx.AddSelection(ctx.From + "." + Column, _field);
        }

        public virtual void ExtractDeepFields(IEntityManager em, IModelSchema schema, string type, List<OutObject> rows)
        {
            throw new NotImplementedException();
        }

        public string GetFieldName()
        {
            return _field;
        }

        public override string ToString()
        {
            return _field;
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
