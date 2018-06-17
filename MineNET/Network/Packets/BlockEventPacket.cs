using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class BlockEventPacket : DataPacket
    {
        public override byte PacketID
        {
            get
            {
                return ProtocolInfo.BLOCK_EVENT_PACKET;
            }
        }

        public Vector3i Position { get; set; }
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

            this.Position = this.ReadBlockVector3i();
            this.EventType = this.ReadVarInt();
            this.EventData = this.ReadVarInt();
        }
    }
}
