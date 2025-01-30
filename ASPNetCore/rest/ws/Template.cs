using gqltosql.schema;

namespace rest.ws
{
    public class Template
    {
        private string _hash;
        private TemplateType[] _types;
        private int[] _mapping;
        private int[] _channelMapping;
        private int[] _rpcMapping;

        private TemplateUsage[] _usages;
        private TemplateClazz[] _channelInfo;
        private TemplateClazz[] _rpcInfo;

        public Template(int types, int usages, int channels, int idx)
        {
            this.Usages = new TemplateUsage[usages];
            this.Types = new TemplateType[types];
            this.Mapping = new int[SchemaConstants._TOTAL_COUNT];
            this.ChannelInfo = new TemplateClazz[channels];
            this.ChannelMapping = new int[ChannelConstants._CHANNEL_COUNT];
            this.RpcInfo = new TemplateClazz[rpcs];
            this.RpcMapping = new int[RPCConstants._CLASS_COUNT];
        }

        public string Hash { get { return _hash; } set { _hash = value; } }

        public TemplateType[] Types { get => _types; set => _types = value; }
        public int[] Mapping { get => _mapping; set => _mapping = value; }
        public int[] ChannelMapping { get => _channelMapping; set => _channelMapping = value; }
        public int[] RpcMapping { get => _rpcMapping; set => _rpcMapping = value; }
        public TemplateUsage[] Usages { get => _usages; set => _usages = value; }
        public TemplateClazz[] ChannelInfo { get => _channelInfo; set => _channelInfo = value; }
        public TemplateClazz[] RpcInfo { get => _rpcInfo; set => _rpcInfo = value; }

        public TemplateType GetType(int index)
        {
            return _types[index];
        }

        public TemplateUsage GetUsageType(int index)
        {
            return _usages[index];
        }

        public void SetTypeTemplate(int idx, TemplateType type)
        {
            _types[idx] = type;
            if(type.Model != null)
            {
                _mapping[type.Model.GetIndex()] = idx;
            }
        }

        public void SetUsageTemplate(int idx, TemplateUsage ut)
        {
            _usages[idx] = ut;
        }

        public int ToClientTypeIdx(int serverIdx)
        {
            return _mapping[serverIdx];
        }

        public void SetChannelTemplate(int i, TemplateClazz tc)
        {
            _channelInfo[i] = tc;
            DClazz ch = tc.Clazz;
            if (ch != null)
            {
                _channelMapping[ch.Index] = i;
            }
        }

        public int GetClientChannelIndex(int serverIdx)
        {
            return _channelMapping[serverIdx];
        }

        public TemplateClazz GetChannel(int chIdx)
        {
            TemplateClazz channel = _channelInfo[chIdx];
            return channel;
        }

        public void SetRPCTemplate(int i, TemplateClazz tc)
        {
            _rpcInfo[i] = tc;
            DClazz ch = tc.Clazz;
            if (ch != null)
            {
                _rpcMapping[ch.Index] = i;
            }
        }

        public int GetClientRPCIndex(int serverIdx)
        {
            return _rpcMapping[serverIdx];
        }

        public TemplateClazz GetRPCMethod(int chIdx)
        {
            TemplateClazz clazz = _rpcInfo[chIdx];
            return clazz;
        }
    }
}
