using System.Text;
using d3e.core;

namespace gqltosql
{
    public class SqlQuery
    {
        private QueryReader _reader;
        private HashSet<string> _columnAliases = new HashSet<string>();
        private List<string> _columns = new List<string>();
        private List<string> _joins = new List<string>();
        private List<string> _where = new List<string>();
        private List<string> _groupsBys = new List<string>();

        public SqlQuery(QueryReader reader)
        {
            this._reader = reader;
        }

        public void SetFrom(string tableName, string alias)
        {
            _joins.Add(tableName + "" + alias);
        }

        public void AddCustomFieldSelection(int type, long id, String customFieldColumn, String parentField,
            String valueField, String column, String field, QueryTypeReader reader)
        {
            _reader.AddCustomField(type, id, parentField, valueField, field, _columns.Count);
            _columns.Add(column + " as " + PrepareFieldAlias(field, 0));
        }

        public void AddGeoSelection(string column, string field, QueryTypeReader reader)
        {
            reader.AddGeo(field, _columns.Count);
            string alias = PrepareFieldAlias(field, 0);
            _columns.Add("(st_x(" + column + "::geometry) || ',' || st_y(" + column + "::geometry)) as " + alias);
        }

        public void AddSelection(string column, string field, QueryTypeReader reader)
        {
            reader.Add(field, _columns.Count);
            _columns.Add(column + " as " + PrepareFieldAlias(field, 0));
        }

        private string PrepareFieldAlias(string field, int i)
        {
            string alias;
            if (i == 0)
            {
                alias = field;
            }
            else
            {
                alias = field + "_" + i;
            }
            if (_columnAliases.Contains(alias))
            {
                return PrepareFieldAlias(field, i + 1);
            }
            _columnAliases.Add(alias);
            return alias;
        }

        public QueryReader AddRefSelection(string column, string field, QueryTypeReader reader)
        {
            QueryReader r = reader.AddRef(field, _columns.Count);
            _columns.Add(column + " as " + PrepareFieldAlias(field, 0));
            return r;
        }

        public void AddJoin(string tableName, string join, string on)
        {
            _joins.Add(tableName + " " + join + " on " + on);
        }

        public void AddWhere(string condition)
        {
            _where.Add(condition);
        }

        public void AddGroupBy(string column)
        {
            if (_groupsBys.Contains(column))
            {
                return;
            }
            _groupsBys.Add(column);
        }

        public string CreateSQL()
        {
            StringBuilder b = new StringBuilder();
            b.Append("select ").Append(string.Join(", ", _columns)).Append(" from ")
              .Append(string.Join(" left join ", _joins));

            if (_where.Count > 0)
            {
                b.Append(" where ").Append(string.Join(" and ", _where));
            }

            if (_groupsBys.Count > 0)
            {
                b.Append(" group by ").Append(string.Join(", ", _groupsBys));
            }

            return b.ToString();
        }

        public QueryReader Reader
        {
            get { return _reader; }
        }
    }
}
