using System;

namespace MineNET.Events.NetworkEvents
{
    public sealed class NetworkEvent
    {
        public event EventHandler<RakNetPacketReceiveEventArgs> RakNetPacketReceive;
        public void OnRakNetPacketReceive(object sender, RakNetPacketReceiveEventArgs e)
        {
            this.RakNetPacketReceive?.Invoke(sender, e);
        }

        public event EventHandler<RakNetPacketSendEventArgs> RakNetPacketSend;
        public void OnRakNetPacketSend(object sender, RakNetPacketSendEventArgs e)
        {
            this.RakNetPacketSend?.Invoke(sender, e);
        }

        public event EventHandler<RakNetDataPacketReceiveEventArgs> RakNetDataPacketReceive;
        public void OnRakNetDataPacketReceive(object sender, RakNetDataPacketReceiveEventArgs e)
        {
            this.RakNetDataPacketReceive?.Invoke(sender, e);
        }

        public event EventHandler<RakNetDataPacketSendEventArgs> RakNetDataPacketSend;
        public void OnRakNetDataPacketSend(object sender, RakNetDataPacketSendEventArgs e)
        {
            this.RakNetDataPacketSend?.Invoke(sender, e);
        }

        public event EventHandler<CreateSessionEventArgs> CreateSession;
        public void OnCreateSession(object sender, CreateSessionEventArgs e)
        {
            this.CreateSession?.Invoke(sender, e);
        }
    }
}
