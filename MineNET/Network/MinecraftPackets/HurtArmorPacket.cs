namespace MineNET.Network.MinecraftPackets
{
    public class HurtArmorPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.HURT_ARMOR_PACKET;

        public int Health { get; set; }

        protected override void EncodePayload()
        {
            this.WriteVarInt(this.Health);
        }

        protected override void DecodePayload()
        {
            this.Health = this.ReadVarInt();
        }
    }
}
