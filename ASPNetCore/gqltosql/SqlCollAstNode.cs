using java.util.concurrent;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using gqltosql.schema;

namespace gqltosql
{
    public class SqlCollAstNode : SqlAstNode
    {
        private string _collTable;
        private string _idColumn;
        private string _column;

        public SqlCollAstNode(IModelSchema schema, string path, int type, string table, string collTable, string idColumn,
            string column) : base(schema, path, type, table, false)
        {
            this._collTable = collTable;
            this._idColumn = idColumn;
            this._column = column;
        }

        public override SqlQueryContext CreateCtx()
        {
            SqlQueryContext ctx = new SqlQueryContext(this, -1);
            string from = ctx.NextAlias();
            ctx.GetQuery().From(_collTable, from);
            ctx.AddSelection(from + "." + _idColumn, "_parent");
            ctx.GetQuery().addWhere(from + "." + _idColumn + " in (:ids)");
            if (IsEmpty())
            {
                ctx.AddSelection(from + "." + _column, "id");
                ctx.SubType(Type);
                return ctx;
            }
            else
            {
                SqlQueryContext sub = ctx.SubType(Type);
                string join = sub.From;
                ctx.AddJoin(TableName, join, join + "._id = " + from + "." + _column);
                return ctx;
            }
        }
    }
}
