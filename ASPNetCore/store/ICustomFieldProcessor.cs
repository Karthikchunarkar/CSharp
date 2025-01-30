using System.Collections;
using System.Data;
using System.Text;
using gqltosql;
using gqltosql.schema;

namespace store
{
    public interface ICustomFieldProcessor<T>
    {
        void Insert(D3EQuery query, DModel<T> type, T _this, List<String> cols, List<String> values, List<Object> args);

        void Update(D3EQuery query, DModel<T> type, T _this, List<String> updates, List<Object> args);

        void SelectAll(StringBuilder sb, DModel<T> type, long id, IList selectedFields, List<String> joins,
                AliasGenerator ag, String alias);

        int ReadObject(IDataReader rs, int index, T obj, DField<object, object> field, long customFieldId);

        void AddGqlToSqlFields(GqlToSql gql, SqlAstNode node, Field field, DField<object, object> df);

        DField<object, object> GetDField(String field);
    }
}
