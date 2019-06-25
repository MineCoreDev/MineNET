using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MineNET.I18n;
using MineNET.Network.RakNet.Interfaces;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace MineNET.Network.RakNet
{
    public class RakNetServer : IRakNetServer
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        private ConcurrentDictionary<IPEndPoint, IRakNetSession> _sessions =
            new ConcurrentDictionary<IPEndPoint, IRakNetSession>();

        private UdpClient _client;
        private Task _clientTask;
        private CancellationTokenSource _cancellation;

        public bool Start(IPEndPoint endPoint)
        {
            LoggingConfiguration configuration = new LoggingConfiguration();
            configuration.AddTarget("console", new ConsoleTarget());
            configuration.AddRule(LogLevel.Trace, LogLevel.Fatal, "console");
            LogManager.Configuration = configuration;

            _client = new UdpClient(endPoint);
            _client.DontFragment = true;
            _client.Client.ReceiveTimeout = 500;

            _logger.Info(StringManager.GetTextContainer("raknet.task.start", endPoint.Port));

            _cancellation = new CancellationTokenSource();
            _clientTask = Task.Factory.StartNew(OnReceive, _cancellation.Token, TaskCreationOptions.LongRunning,
                TaskScheduler.Default);

            return true;
        }

        public bool Stop()
        {
            _cancellation.Cancel();
            _client.Close();
            return true;
        }

        public void AddSession(IRakNetSession session)
        {
            throw new NotImplementedException();
        }

        public void RemoveSession(IRakNetSession session)
        {
            throw new NotImplementedException();
        }

        public IRakNetSession GetSession(IPEndPoint endPoint)
        {
            throw new NotImplementedException();
        }

        public IRakNetSession[] GetSessions()
        {
            throw new NotImplementedException();
        }

        private void OnReceive()
        {
            while (true)
            {
                try
                {
                    IPEndPoint endPoint = null;
                    byte[] receive = _client.Receive(ref endPoint);
                }
                catch (Exception e)
                {
                    _logger.Error(StringManager.GetTextContainer("raknet.task.error", e.Message));
                }

                if (_cancellation.IsCancellationRequested)
                {
                    _logger.Info(StringManager.GetTextContainer("raknet.task.stop"));
                    _cancellation.Token.ThrowIfCancellationRequested();
                }
            }
        }
    }
}