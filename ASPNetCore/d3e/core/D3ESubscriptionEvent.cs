using store;

namespace d3e.core
{
    public class D3ESubscriptionEvent<T>
    {
        public T Model;
        public StoreEventType ChangeType;
    }
}
