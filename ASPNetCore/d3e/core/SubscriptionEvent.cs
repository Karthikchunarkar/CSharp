namespace d3e.core
{
    public class SubscriptionEvent
    {
        private string _id;

        private object _obj;

        public SubscriptionEvent(string id, object obj)
        {
            this._id = id;
            this._obj = obj;
        }

        public string GetId()
        {
            return _id;
        }

        public object GetObj()
        {
            return _obj;
        }
    }
}
