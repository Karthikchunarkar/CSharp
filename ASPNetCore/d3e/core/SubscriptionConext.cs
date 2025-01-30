namespace d3e.core
{
    public class SubscriptionConext
    {
        private string _Id;

        private readonly Type _readType;

        private readonly string _input;

        public SubscriptionConext(Type readType, string input)
        {
            this._readType = readType;
            this._input = input;
        }

        public void SetId(string id)
        {
            _Id = id;
        }

        public string GetId()
        {
            return _Id;
        }

        public string GetInput()
        {
            return this._input;
        }

        public Type GetReadType()
        {
            return this._readType;
        }
    }
}
