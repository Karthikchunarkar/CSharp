namespace rest.ws
{
    public class UsageField
    {
        private int _field;

        private UsageType[] _types;

        public UsageField(int field,  UsageType[] types)
        {
            this._field = field;
            this._types = types;
        }

        public int Field { get { return _field; } }


        public UsageType[] Types
        {
            get { return _types; }


        }

        public UsageType GetType(int field) 
        {
            foreach (var type in _types)
            {
                if(type.Type == field)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
