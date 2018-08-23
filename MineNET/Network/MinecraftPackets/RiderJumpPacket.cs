namespace MineNET.Network.MinecraftPackets
{
    public class RiderJumpPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.RIDER_JUMP_PACKET;

        public int JumpStrength { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteVarInt(this.JumpStrength);
        }

        public override void Decode()
        {
            base.Decode();

            this.JumpStrength = this.ReadVarInt();
        }
    }
}
