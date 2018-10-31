using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class BlockEventPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.BLOCK_EVENT_PACKET;

        public BlockCoordinate3D Position { get; set; }
        public int EventType { get; set; }
        public int EventData { get; set; }

        protected override void EncodePayload()
        {
            this.WriteBlockVector3(this.Position);
            this.WriteVarInt(this.EventType);
            this.WriteVarInt(this.EventData);
        }

        protected override void DecodePayload()
        {
            this.Position = this.ReadBlockVector3();
            this.EventType = this.ReadVarInt();
            this.EventData = this.ReadVarInt();
        }
    }
}
