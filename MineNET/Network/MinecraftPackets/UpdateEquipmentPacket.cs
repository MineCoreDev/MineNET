namespace MineNET.Network.MinecraftPackets
{
    public class UpdateEquipmentPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.UPDATE_EQUIPMENT_PACKET;

        public byte WindowId { get; set; }
        public byte WindowType { get; set; }
        public int UnknownVarInt { get; set; }
        public long EntityUniqueId { get; set; }
        public byte[] Namedtag { get; set; }

        protected override void EncodePayload()
        {
            this.WriteByte(this.WindowId);
            this.WriteByte(this.WindowType);
            this.WriteVarInt(this.UnknownVarInt);
            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteBytes(this.Namedtag);
        }

        protected override void DecodePayload()
        {

        }
    }
}
