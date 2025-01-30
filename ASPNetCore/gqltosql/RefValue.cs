namespace gqltosql
{
    public class RefValue : IValue
    {
        private string _field;
        private QueryReader _reader;

        public RefValue(string field, bool embedded, int index)
        {
            this._field = field;
            this._reader = new QueryReader(index, embedded);
        }

        public QueryReader Reader { get { return _reader; } }

        public virtual object Read(object[] row, OutObject obj)
        {
            OutObject read = _reader.Read(row, new Dictionary<long, OutObject>());
            obj.Add(_field, read);
            return read;
        }

        public override string ToString()
        {
            return _field;
        }
    }
}
