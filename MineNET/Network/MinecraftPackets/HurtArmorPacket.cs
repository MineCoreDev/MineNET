namespace MineNET.Network.MinecraftPackets
{
    public class HurtArmorPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.HURT_ARMOR_PACKET;

        public int Health { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteVarInt(this.Health);
        }

        public override void Decode()
        {
            base.Decode();

            this.Health = this.ReadVarInt();
        }
    }
}
