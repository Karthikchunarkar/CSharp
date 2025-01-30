namespace rest.ws
{
    public class UsageType
    {
        private int _type;

        private UsageField[] _fields;

        public UsageType(int type, int fieldCount)
        {
            this._type = type;
            this._fields = new UsageField[fieldCount];
        }

        public int Type { get { return _type; } }

        public UsageField[] Fields
        {
            get { return _fields; }
        }
    }
}
