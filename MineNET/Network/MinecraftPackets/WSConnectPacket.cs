using System;

namespace MineNET.Network.MinecraftPackets
{
    public class WSConnectPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.W_S_CONNECT_PACKET;

        public String ServerUri { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.ServerUri);
        }

        protected override void DecodePayload()
        {
            this.ServerUri = this.ReadString();
        }
    }
}
