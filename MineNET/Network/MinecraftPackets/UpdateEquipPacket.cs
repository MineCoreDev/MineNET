namespace MineNET.Network.MinecraftPackets
{
    public class UpdateEquipPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.UPDATE_EQUIPMENT_PACKET;

        public byte WindowId { get; set; }
        public byte WindowType { get; set; }
        public int UnknownVarInt { get; set; }
        public long EntityUniqueId { get; set; }
        public byte[] Namedtag { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.WindowId);
            this.WriteByte(this.WindowType);
            this.WriteVarInt(this.UnknownVarInt);
            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteBytes(this.Namedtag);
        }
    }
}
