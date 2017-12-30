using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace MineCraftPENetwork.Server
{
    public class RakNetServer
    {
        protected int port;
        protected string ipInterface;

        protected bool shutdown;

        /** @var \Threaded */
        protected Queue<byte[]> externalQueue = new Queue<byte[]>();
        /** @var \Threaded */
        protected Queue<byte[]> internalQueue = new Queue<byte[]>();

        protected string mainPath;

        /** @var int */
        protected long serverId = 0;

        public RakNetServer(int port, string ipInterface = "0.0.0.0", bool autoStart = true)
        {
            this.port = port;
            if (port < 1 || port > 65536)
            {
                throw new Exception("Invalid port range");
            }

            this.ipInterface = ipInterface;

            serverId = new Random().Next(0, int.MaxValue);

            shutdown = false;

            if (autoStart)
            {
                Run();
            }
        }

        public bool IsShutdown()
        {
            return shutdown == true;
        }

        public void Shutdown()
        {
            shutdown = true;
        }

        public int GetPort()
        {
            return port;
        }

        public string GetInterface()
        {
            return ipInterface;
        }

        public long GetServerId()
        {
            return serverId;
        }

        public void PushMainToThreadPacket(byte[] str)
        {
            internalQueue.Enqueue(str);
        }

        public byte[] ReadMainToThreadPacket()
        {
            if (internalQueue.Count > 0)
            {
                return internalQueue.Dequeue();
            }
            return new byte[0];
        }

        public void PushThreadToMainPacket(byte[] str)
        {
            externalQueue.Enqueue(str);
        }

        public byte[] ReadThreadToMainPacket()
        {
            if (externalQueue.Count > 0)
            {
                return externalQueue.Dequeue();
            }
            return new byte[0];
        }

        public void Run()
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Any, port);
            var socket = new UdpClient(point);
            //PHP BufferSize
            //socket.Client.ReceiveBufferSize = 1024 * 1024 * 3;
            //socket.Client.SendBufferSize = 4096;
            socket.Client.ReceiveBufferSize = int.MaxValue;
            socket.Client.SendBufferSize = int.MaxValue;
            socket.DontFragment = false;
            socket.EnableBroadcast = false;

            uint IOC_IN = 0x80000000;
            uint IOC_VENDOR = 0x18000000;
            uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
            socket.Client.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);

            new SessionManager(this, socket);
        }
    }
}
