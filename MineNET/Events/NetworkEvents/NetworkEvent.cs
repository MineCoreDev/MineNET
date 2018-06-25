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

        public event EventHandler<RakNetEncapsulatedReceiveEventArgs> RakNetEncapsulatedReceive;
        public void OnRakNetEncapsulatedReceive(object sender, RakNetEncapsulatedReceiveEventArgs e)
        {
            this.RakNetEncapsulatedReceive?.Invoke(sender, e);
        }

        public event EventHandler<RakNetEncapsulatedSendEventArgs> RakNetEncapsulatedSend;
        public void OnRakNetEncapsulatedSend(object sender, RakNetEncapsulatedSendEventArgs e)
        {
            this.RakNetEncapsulatedSend?.Invoke(sender, e);
        }

        public event EventHandler<CreateSessionEventArgs> CreateSession;
        public void OnCreateSession(object sender, CreateSessionEventArgs e)
        {
            this.CreateSession?.Invoke(sender, e);
        }

        public event EventHandler<CloseSessionEventArgs> CloseSession;
        public void OnCloseSession(object sender, CloseSessionEventArgs e)
        {
            this.CloseSession?.Invoke(sender, e);
        }
    }
}
