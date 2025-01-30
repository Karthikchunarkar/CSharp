using Newtonsoft.Json.Linq;
using gqltosql.schema;
using store;

namespace gqltosql
{
    public interface ISqlColumn
    {
        string GetFieldName();

        void AddColumn(SqlTable table, SqlQueryContext ctx);

        SqlAstNode GetSubQuery();

        void UpdateSubField(Dictionary<long, OutObject> parents, List<OutObject> all);

        void ExtractDeepFields(IEntityManager em, IModelSchema schema, string type, List<OutObject> rows);
    }
}
