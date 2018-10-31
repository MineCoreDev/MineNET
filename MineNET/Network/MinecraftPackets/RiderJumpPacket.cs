namespace MineNET.Network.MinecraftPackets
{
    public class RiderJumpPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.RIDER_JUMP_PACKET;

        public int JumpStrength { get; set; }

        protected override void EncodePayload()
        {
            this.WriteVarInt(this.JumpStrength);
        }

        protected override void DecodePayload()
        {
            this.JumpStrength = this.ReadVarInt();
        }
    }
}
