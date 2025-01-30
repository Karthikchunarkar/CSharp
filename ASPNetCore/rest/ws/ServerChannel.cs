namespace rest.ws
{
    public class ServerChannel<C>
    {
        private readonly ThreadLocal<C> _client = new ThreadLocal<C>();

        public void SetClient(C client)
        {
            _client.Value = client;
        }

        public void RemoveClient()
        {
            _client.Value = default;
        }

        public C GetClient()
        {
            return _client.Value;
        }

        public bool OnConnect(C client)
        {
            return true;
        }

        public void OnDisconnect(C client)
        {
            // Implementation for handling client disconnect
        }
    }
}
