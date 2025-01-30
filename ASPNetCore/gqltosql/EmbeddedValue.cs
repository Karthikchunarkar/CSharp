namespace gqltosql
{
    public class EmbeddedValue : RefValue
    {
        public EmbeddedValue(string field) : base(field, true, -1)
        {

        }

        public override object Read(object[] row, OutObject obj)
        {
            OutObject read = (OutObject)base.Read(row, obj);
            if (read != null)
            {
                read.Id = obj.Id;
            }
            return read;
        }
    }
}
