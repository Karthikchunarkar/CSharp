namespace rest.ws
{
    public abstract class AbstractClientProxy
    {
        private static readonly int CHANNEL_MESSAGE = -2;

        protected void Send(RocketMessage val)
        {
            val.Flush();
        }

        protected RocketMessage CreateMessage(int chIdx, int msgIdx)
        {
            ClientSession session = GetSession();
            RocketMessage outer = session.CreateMessage();
            outer.WriteInt(CHANNEL_MESSAGE);
            Template template = session.Template;
            int channelIndex = template.GetClientChannelIndex(chIdx);
            outer.WriteInt(channelIndex);
            TemplateClazz channel = template.GetChannel(channelIndex); 
            int msgIndex = channel.GetClientMethodIndex(msgIdx);
            outer.WriteInt(msgIndex);
            return outer;
        }

        public abstract ClientSession GetSession();
    }
}
