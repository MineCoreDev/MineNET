using System;

namespace MineNET.Network.MinecraftPackets
{
    public class WSConnectPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.W_S_CONNECT_PACKET;

        public String ServerUri { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.ServerUri);
        }

        public override void Decode()
        {
            base.Decode();

            this.ServerUri = this.ReadString();
        }
    }
}
