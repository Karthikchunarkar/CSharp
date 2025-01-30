using gqltosql.schema;

namespace gqltosql
{
    public class SqlAstCustomFieldNode : SqlAstNode
    {
        public SqlAstCustomFieldNode(IModelSchema schema, string path, int type, string table, string idColumn) : base(schema, path, type, table, false)
        {
            foreach (var item in Tables.Values)
            {
                item.IdColumn = idColumn;
            }
        }
    }
}
