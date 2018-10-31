namespace MineNET.Network.MinecraftPackets
{
    public class SetEntityLinkPacket : MinecraftPacket
    {
        public const byte TYPE_REMOVE = 0;
        public const byte TYPE_RIDER = 1;
        public const byte TYPE_PASSENGER = 2;

        public override byte PacketID { get; } = MinecraftProtocol.SET_ENTITY_LINK_PACKET;

        public long RiddenEid { get; set; }
        public long RiderEid { get; set; }
        public byte Type { get; set; }
        public bool Immediate { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityUniqueId(this.RiddenEid);
            this.WriteEntityUniqueId(this.RiderEid);
            this.WriteByte(this.Type);
            this.WriteBool(this.Immediate);
        }

        protected override void DecodePayload()
        {

        }
    }
}
