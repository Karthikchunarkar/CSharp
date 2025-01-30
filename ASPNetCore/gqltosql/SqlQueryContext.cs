
namespace gqltosql
{
    public class SqlQueryContext
    {
        private SqlQuery _query;
        private QueryReader _reader;
        private QueryTypeReader _typeReader;
        private AliasGenerator _ag;
        private string _prefix;
        private string _alias;
        private Dictionary<string, string> _tableAliases;

        public SqlQueryContext(SqlAstNode node, int index)
        {
            this._query = new SqlQuery(new QueryReader(index, node.Embedded));
            this._reader = this._query.Reader;
            this._typeReader = this._reader.GetTypeReader(-1);
            this._ag = new AliasGenerator();
            this._tableAliases = new Dictionary<string, string>();
            CreateAllTablesAliases(this._prefix, node);
            this._alias = GetTableAlias(node.Type.ToString());
        }

        private void CreateAllTablesAliases(string? prefix, SqlAstNode node)
        {
            foreach (var item in node.Tables)
            {
                int type = item.Key;
                SqlTable table = item.Value;
                string localPrefix = prefix == null ? type.ToString() : prefix + "." + type;
                string a = NextAlias;
                _tableAliases[localPrefix] = a;
                table.Columns.ForEach(c =>
                {
                     if(c is RefCollSqlColumn val)
                    {
                        SqlAstNode sub = val.Sub;
                        CreateAllTablesAliases(localPrefix+ "." + c.GetFieldName(), sub);
                        if (sub.Embedded)
                        {
                            string embeddprefix = localPrefix + "." + c.GetFieldName() + "." + sub.Type;
                            _tableAliases[embeddprefix] = a;
                        }
                    }
                });
            }
        }

        public SqlQueryContext(SqlQueryContext from)
        {
            this._query = from._query;
            this._reader= from._reader;
            this._typeReader = from._typeReader;
            this._ag = from._ag;
            this._tableAliases = from._tableAliases;
            this._alias = from._alias;
            this._prefix = from._prefix;
        }

        public QueryTypeReader TypeReader { get { return _typeReader; } }

        public SqlQueryContext SubType(int type)
        {
            SqlQueryContext ctx = new SqlQueryContext(this);
            ctx._prefix = this._prefix == null ? type.ToString() : this._prefix + "." + type;
            ctx._alias = _tableAliases[ctx._prefix];
            ctx._typeReader = _reader.GetTypeReader(type);
            return ctx;
        }

        public SqlQueryContext SubPrefix(string prefix)
        {
            SqlQueryContext ctx = new SqlQueryContext(this);
            ctx._prefix = this._prefix == null ? prefix : this._prefix + "." + prefix;
            ctx._alias = _tableAliases[ctx._prefix];
            return ctx;
        }

        public SqlQueryContext SubReader(QueryReader reader)
        {
            SqlQueryContext ctx = new SqlQueryContext(this);
            ctx._reader = reader;
            return ctx;
        }

        public string From { get { return _alias; } set { _alias = value; } }

        public void AddCustomFieldSelection(int type, long id, string customFieldColumn, string parentField,
            string valueField, string column, string field)
        {
            _query.AddCustomFieldSelection(type, id, customFieldColumn, parentField, valueField, column, field, _typeReader);
        }

        public void AddGeoSelection(string column, string field)
        {
            _query.AddGeoSelection(column, field, _typeReader);
        }

        public void AddSelection(string value, string field)
        {
            _query.AddSelection(value, field, _typeReader);
        }

        public QueryReader AddRefSelection(string column, string field)
        {
            return _query.AddRefSelection(column, field, _typeReader);
        }

        public void AddJoin(string tableName, string join, String on)
        {
            _query.AddJoin(tableName, join, on);
        }

        public void AddGroupBy(string column)
        {
            _query.AddGroupBy(column);
        }

        public string NextAlias { get { return _ag.Next(); } }

        public SqlQuery Query { get { return _query; } }

        public string GetTableAlias(string type)
        {
            string localPrefix = this._prefix == null ? type : this._prefix + "." + type;
            string a = _tableAliases[localPrefix];
            return a;
        }

        public void AddSqlColumns(SqlAstNode sub)
        {
            sub.SelectColumns(this);
        }
    }
}
