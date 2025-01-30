using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using Microsoft.OpenApi.Extensions;
using d3e.core;

namespace rest.ws
{
    public class ClientSession : RocketSender
    {
        private WebSocket _webSocket;
        private MemoryStream _stream = new MemoryStream();
        private Template _template;
        private long _userId;
        private string _userType;
        private string _token;
        private string _deviceToken;
        private readonly object _lock = new object();
        private Dictionary<string, AbstractClientProxy> _proxies = new Dictionary<string, AbstractClientProxy>();
        private List<byte[]> _queue = new List<byte[]>();
        private BlockingCollection<RocketMessage> _inputMessages = new BlockingCollection<RocketMessage>();
        private long _timeout;
        private readonly Action<ClientSession, RocketMessage> _executer;
        private readonly string _sessionId;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public ClientSession(WebSocket webSocket, Action<ClientSession, RocketMessage> executer)
        {
            _webSocket = webSocket;
            _executer = executer;
            _sessionId = Guid.NewGuid().ToString();
            StartMessageProcessing();
        }

        public Template Template { get { return _template; } set { _template = value; } }
        public string SessionId { get { return _sessionId; } }
        public string Token { get { return _token; }  set { _token = value; } }
        public WebSocket Session { get { return _webSocket; } }

        public MemoryStream Stream => _stream;

        public long UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string UserType { get {  return _userType; }  set { _userType = value; } }
        public long Timeout
        {
            get => _timeout;
            set => _timeout = value;
        }

        public void Lock()
        {
            Monitor.Enter(_lock);
        }

        public bool IsLocked()
        {
            return Monitor.IsEntered(_lock);
        }

        public void Unlock()
        {
            Monitor.Exit(_lock);
        }

        private async void StartMessageProcessing()
        {
            await Task.Run(async () =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        var message = _inputMessages.Take(_cancellationTokenSource.Token);
                        MDC.Set("userId", _userId.ToString());
                        MDC.Set("http", SessionId);
                        MDC.Set("reqid", message.ReqId.ToString());
                        {
                            var startTime = message.Time;
                            message.Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                            Log.Info($"Message was queued ({message.Time - startTime}ms). Started executing");

                            _executer(this, message);

                            startTime = message.Time;
                            message.Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                            Log.Info($"Execution done ({message.Time - startTime}ms). Queue size: {_inputMessages.Count}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Error processing message: " + ex.Message);
                    }
                }
            });
        }

        public Template GetTemplate()
        {
            return Template;
        }

        public string GetToken()
        {
            return _token;
        }

        public void SendMessage(byte[] msg, int msgId, bool system)
        {
            if (msgId == -0)
            {
                Monitor.Enter(_lock);
            }
            try
            {
                if (_webSocket == null)
                {
                    _queue.Add(msg);
                }
                else
                {
                    _webSocket.SendAsync(msg,
                        WebSocketMessageType.Binary,
                        true,
                        CancellationToken.None);
                }
            }
            finally
            {
                Unlock();
            }
        }

        public void SetSession(WebSocket socket)
        {
            this._webSocket = socket;
            if (_webSocket != null)
            {
                foreach (var val in _queue)
                {
                    _webSocket.SendAsync(val,
                        WebSocketMessageType.Binary,
                        true,
                        CancellationToken.None);
                }
                _queue.Clear();
            }
        }

        public Task SendPingAsync() => Task.CompletedTask;

        public long TimeOut { get { return _timeout; } set { _timeout = value; } }

        public string DeviceToken { get => _deviceToken; set => _deviceToken = value; }
        public Dictionary<string, AbstractClientProxy> Proxies { get => _proxies; set => _proxies = value; }

        public void Logout()
        {
            this._userId = 0;
            this._userType = null;
            this._token = null;
        }

        public void AddMessageToQueue()
        {
            RocketMessage msg = new RocketMessage(this._stream.ToArray(), this, false);
            msg.ReqId = msg.ReadInt();
            msg.Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            MDC.Set("reqid", msg.ReqId);
            Log.Info("Client Message added. size:" + this._inputMessages.Count);
            this._inputMessages.Add(msg);
            this._stream = new MemoryStream();
        }

        public RocketMessage CreateMessage()
        {
            return new RocketMessage(new byte[] { }, this, false);
        }

        public string GetClientAddress()
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                return _webSocket.GetHashCode().ToString();
            }
            return "Unknown";
        }

        public string GetSessionId()
        {
            return _webSocket.State.GetDisplayName();
        }
    }
}
