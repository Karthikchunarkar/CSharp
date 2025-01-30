using d3e.core;
using gqltosql.schema;
using store;

namespace rest.ws
{
    public abstract class AbstractChannels
    {
        private Dictionary<string, ServerChannel<object>> _allChannels;

        public bool Connect(DClazz dm, ClientSession ses, EntityHelperService helperService)
        {
            Log.Info("Channel Connected: " + dm.Name);AbstractClientProxy
            ServerChannel<object> channel = _allChannels[dm.Name];
            AbstractClientProxy proxy = GetChannelClientProxy(dm.Index, ses, helperService, ses.Template);
            if (proxy == null)
            {
                Log.Info("Unable to create Client Proxy for '" + dm.Name + "' idx: " + dm.Index);
                return false;
            }
            bool val = channel.OnConnect(proxy);
            if (val)
            {
                ses.Proxies[dm.Name] = proxy;
            }
            return val;
        }

        public void Disconnect(DClazz dm, ClientSession ses)
        {
            Log.Info("Channel disconnected: " + dm.Name);
            ServerChannel<object> channel = _allChannels[dm.Name];
            if (ses.Proxies.TryGetValue(dm.Name, out var abstractClient))
            {
                channel.OnDisconnect(abstractClient);
            }
        }

        public void Disconnect(ClientSession ses)
        {
            foreach (var item in ses.Proxies)
            {
                ServerChannel<object> channel = _allChannels[item.Key];
                channel.OnDisconnect(item.Value);
            }
            ses.Proxies.Clear();
            // TODO Nikhil
        }

        public void OnMessage(DClazz dm, int msgSrvIdx, ClientSession sesssion, RocketInputContext ctx, EntityHelperService helperService)
        {
            ServerChannel<object> channel = _allChannels[dm.Name];
            if (!sesssion.Proxies.ContainsKey(dm.Name))
            {
                Connect(dm, sesssion, helperService);
            }
            channel.SetClient(sesssion.Proxies[dm.Name]);
            try
            {
                HandleChannelMessage(dm.Index, msgSrvIdx, ctx, channel);
            }
            finally
            {
                channel.RemoveClient();
            }
        }

        protected abstract void HandleChannelMessage(int idx, int msgSrvIdx, RocketInputContext ctx,
     ServerChannel<object> channel);

        protected abstract AbstractClientProxy GetChannelClientProxy(int idx, ClientSession ses, EntityHelperService helperService, Template template);

    }
}
