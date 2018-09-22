using System;
using System.Net;
using System.Net.Sockets;

namespace MineNET.Network
{
    public sealed class UDPSocket : INetworkSocket
    {
        private UdpClient client;
        public UdpClient Socket
        {
            get
            {
                return this.client;
            }
        }

        public void Init(IPEndPoint point)
        {
            try
            {
                this.client = new UdpClient(point);

                this.client.Client.ReceiveBufferSize = int.MaxValue;
                this.client.Client.SendBufferSize = int.MaxValue;
                this.client.DontFragment = false;
                this.client.EnableBroadcast = false;

                uint IOC_IN = 0x80000000;
                uint IOC_VENDOR = 0x18000000;
                uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;

                this.client.Client.IOControl((int) SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);

            }
            catch (SocketException e1)
            {
                throw e1;
            }
        }

        public void Dispose()
        {
            this.client.Close();
            this.client = null;
        }
    }
}
