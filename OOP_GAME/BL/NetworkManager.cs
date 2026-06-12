using OOP_GAME.Model;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace OOP_GAME.BL
{
    /// <summary>
    /// Singleton TCP networking manager for P2P multiplayer.
    /// Host runs a TcpListener; Guest connects via TcpClient.
    /// </summary>
    public class NetworkManager
    {

        private static NetworkManager _instance;
        public static NetworkManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NetworkManager();
                return _instance;
            }
        }

        
        private TcpListener _listener;
        private TcpClient _client;
        private StreamReader _reader;
        private StreamWriter _writer;
        private bool _isRunning;

        public const int Port = 8888;

        
        public event Action<string> OnMessageReceived;
        public event Action<string> OnConnected;       
        public event Action OnDisconnected;
        public event Action<string> OnError;

        // ─── HOST: start listen────────────────
        public async Task StartHostAsync()
        {
            try
            {
                _listener = new TcpListener(IPAddress.Any, Port);
                _listener = new TcpListener(IPAddress.Any, Port);
                _listener.Server.SetSocketOption(
                    SocketOptionLevel.Socket,
                    SocketOptionName.ReuseAddress,
                    true);
                _listener.Start();
                

                _client = await _listener.AcceptTcpClientAsync();
                SetupStreams();

                // wait for CONNECT message from guest
                string connectMsg = await _reader.ReadLineAsync();
                if (connectMsg != null && connectMsg.StartsWith("CONNECT:"))
                {
                    ParseConnectMessage(connectMsg);
                    OnConnected?.Invoke(CurrentSession.Instance.RemotePlayerName);

                    // generate terrain seed and wind, send START
                    var rng = new Random();
                    int seed = rng.Next();
                    float wind = (float)(rng.NextDouble() * 0.16 - 0.08);

                    CurrentSession.Instance.TerrainSeed = seed;
                    CurrentSession.Instance.InitialWind = wind;

                    // send START with game params
                    SendMessage($"START:{seed},{wind}");

                    // send host's appearance so guest knows what sprites to render
                    SendMessage($"SKIN:{CurrentSession.Instance.LocalBodyIndex},{CurrentSession.Instance.LocalCannonIndex}");

                    _isRunning = true;
                    StartReceiveLoop();
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        // ─── GUEST: connect to host ─────────────────────────────────
        public async Task ConnectToHostAsync(string hostIP)
        {
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(IPAddress.Parse(hostIP), Port);
                SetupStreams();

                // send CONNECT with username and appearance
                var session = CurrentSession.Instance;
                SendMessage($"CONNECT:{session.LocalPlayerName},{session.LocalBodyIndex},{session.LocalCannonIndex}");

                // wait for START message
                string startMsg = await _reader.ReadLineAsync();
                if (startMsg != null && startMsg.StartsWith("START:"))
                {
                    string[] parts = startMsg.Substring(6).Split(',');
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0], out int seed) &&
                        float.TryParse(parts[1], out float wind))
                    {
                        session.TerrainSeed = seed;
                        session.InitialWind = wind;
                    }
                }

                // wait for SKIN message from host
                string skinMsg = await _reader.ReadLineAsync();
                if (skinMsg != null && skinMsg.StartsWith("SKIN:"))
                {
                    string[] parts = skinMsg.Substring(5).Split(',');
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0], out int bodyIdx) &&
                        int.TryParse(parts[1], out int cannonIdx))
                    {
                        session.RemoteBodyIndex = bodyIdx;
                        session.RemoteCannonIndex = cannonIdx;
                    }
                }

                OnConnected?.Invoke(CurrentSession.Instance.RemotePlayerName ?? "Host");

                _isRunning = true;
                StartReceiveLoop();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        // ─── send a message ─────────────────────────────────────────
        public void SendMessage(string message)
        {
            try
            {
                if (_writer != null)
                {
                    _writer.WriteLine(message);
                    _writer.Flush();
                }
            }
            catch
            {
                // connection lost — will be caught by receive loop
            }
        }

        // ─── receive loop ───────────────────────────────────────────
        private void StartReceiveLoop()
        {
            Task.Run(async () =>
            {
                try
                {
                    while (_isRunning)
                    {
                        string line = await _reader.ReadLineAsync();
                        if (line == null)
                        {
                            // connection closed
                            _isRunning = false;
                            OnDisconnected?.Invoke();
                            break;
                        }

                        if (line.StartsWith("DISCONNECT"))
                        {
                            _isRunning = false;
                            OnDisconnected?.Invoke();
                            break;
                        }

                        OnMessageReceived?.Invoke(line);
                    }
                }
                catch
                {
                    _isRunning = false;
                    OnDisconnected?.Invoke();
                }
            });
        }

        // ─── helpers ────────────────────────────────────────────────
        private void SetupStreams()
        {
            var stream = _client.GetStream();
            _reader = new StreamReader(stream);
            _writer = new StreamWriter(stream) { AutoFlush = false };
        }

        private void ParseConnectMessage(string msg)
        {
            // CONNECT:<Username>,<BodyIndex>,<CannonIndex>
            string payload = msg.Substring(8); // after "CONNECT:"
            string[] parts = payload.Split(',');

            var session = CurrentSession.Instance;
            session.RemotePlayerName = parts[0];

            if (parts.Length >= 3 &&
                int.TryParse(parts[1], out int bodyIdx) &&
                int.TryParse(parts[2], out int cannonIdx))
            {
                session.RemoteBodyIndex = bodyIdx;
                session.RemoteCannonIndex = cannonIdx;
            }
        }

        // ─── cleanup ────────────────────────────────────────────────
        public void Disconnect()
        {
            try
            {
                _isRunning = false;
                SendMessage("DISCONNECT:");
                _reader?.Close();
                _writer?.Close();
                _client?.Close();
                _listener?.Stop();
            }
            catch { }
        }

        /// <summary>
        /// Reset the singleton so a new connection can be made
        /// </summary>
        public static void Reset()
        {
            _instance?.Disconnect();
            _instance = new NetworkManager();
        }
    }
}
