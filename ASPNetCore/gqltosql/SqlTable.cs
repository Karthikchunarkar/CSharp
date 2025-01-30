namespace gqltosql
{
    public class SqlTable
    {
        private string _table;
        private int _type;
        private List<ISqlColumn> _columns = new List<ISqlColumn>();
        private bool _embedded;
        private string _idColumn;

        public SqlTable(int type, string table, bool embedded)
        {
            this._type = type;
            this._table = table;
            this._embedded = embedded;
        }

        public void AddColumn(ISqlColumn column)
        {
            if (column.GetFieldName().Equals("id"))
            {
                return;
            }
            foreach (var col in _columns)
            {
                if (col.GetFieldName().Equals(col.GetFieldName()))
                {
                    {
                        return;
                    }
                }
            }
            _columns.Add(column);
        }

        public string TableName { get { return _table; } }

        public int Type { get { return _type; } }

        public List<ISqlColumn> Columns { get { return _columns; } }

        public void AddSelections(SqlQueryContext ctx)
        {
            if (!_embedded)
            {
                ctx.AddSelection(ctx.From + "._id", "id");
            }
            Columns.ForEach(c =>
            {
                c.AddColumn(this, ctx);
            });
        }

        public override string ToString()
        {
            return _table;
        }

        public ISqlColumn GetColumn(string name)
        {
            foreach (var col in _columns)
            {
                if(col.GetFieldName().Equals(name))
                {
                    return col;
                }
            }
            return null;
        }

        public string IdColumn { set { _idColumn = value; } }

        public string GetIdColumn(string alias)
        {
            return _idColumn != null ? _idColumn : (alias + "._id");
        }
    }
}
