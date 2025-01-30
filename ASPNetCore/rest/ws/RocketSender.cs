using com.sun.org.apache.xalan.@internal.xsltc.compiler;

namespace rest.ws
{
    public interface RocketSender
    {
        void SendMessage(byte[] msg, int msgId, bool system);

        string GetToken();

        Template GetTemplate();

        string GetSessionId();
    }
}
