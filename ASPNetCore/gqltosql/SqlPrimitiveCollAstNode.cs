using java.util.concurrent;
using gqltosql.schema;

namespace gqltosql
{
    public class SqlPrimitiveCollAstNode : SqlAstNode
    {
        private string _collTable;
        private string _idColumn;
        private string _column;
        private string _field;

        public SqlPrimitiveCollAstNode(IModelSchema schema, string path, string collTable, string idColumn, string column,
            string field) : base(schema, path, 0, null, false)
        {
            this._collTable = collTable;
            this._idColumn = idColumn;
            this._column = column;
            this._field = field;
        }

        public override SqlQueryContext CreateCtx()
        {
            SqlQueryContext ctx = new SqlQueryContext(this, -1);
            string from = ctx.From;
            ctx.GetQuery().SetFrom(_collTable, from);
            ctx.AddSelection(from + "." + _idColumn, "_parent");
            ctx.GetQuery().addWhere(from + "." + _idColumn + " in (:ids)");
            return ctx;
        }

        public override void SelectColumns(SqlQueryContext ctx)
        {
            ctx.AddSelection(ctx.From + "." + _column, _field);
        }
    }
}
