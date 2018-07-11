using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class BlockEventPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.BLOCK_EVENT_PACKET;

        public BlockCoordinate3D Position { get; set; }
        public int EventType { get; set; }
        public int EventData { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteBlockVector3(this.Position);
            this.WriteVarInt(this.EventType);
            this.WriteVarInt(this.EventData);
        }

        public override void Decode()
        {
            base.Decode();

            this.Position = this.ReadBlockVector3();
            this.EventType = this.ReadVarInt();
            this.EventData = this.ReadVarInt();
        }
    }
}
