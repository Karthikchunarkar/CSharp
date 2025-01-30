using Microsoft.EntityFrameworkCore.Metadata.Internal;
using gqltosql.schema;

namespace gqltosql
{
    public class SqlInverseCollAstNode : SqlAstNode
    {
        private string _column;


        public SqlInverseCollAstNode(IModelSchema schema, String path, int type, String table, String column) : base(schema, path, type, table,false)
        {
            this._column = column;
        }

        public override SqlQueryContext CreateCtx()
        {
            SqlQueryContext ctx = new SqlQueryContext(this, 1);     // Setting index as 1 because that's where the object id will be
            SqlQueryContext sub = ctx.SubPrefix(Type.ToString());
            string from = sub.From;
            sub.GetQuery().setFrom(TableName, from);
            sub.AddSelection(from + "." + _column, "_parent");
            sub.AddSelection(from + "._id", "_id");                 // Adding object id. This will be read from index 1
            sub.GetQuery().addWhere(from + "." + _column + " in (:ids)");
            return ctx;
        }
    }
}
