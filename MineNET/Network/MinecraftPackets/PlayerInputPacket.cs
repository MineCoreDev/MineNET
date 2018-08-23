namespace MineNET.Network.MinecraftPackets
{
    public class PlayerInputPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.PLAYER_INPUT_PACKET;

        public float MotionX { get; set; }
        public float MotionY { get; set; }
        public bool Jumping { get; set; }
        public bool Sneaking { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteLFloat(this.MotionX);
            this.WriteLFloat(this.MotionY);
            this.WriteBool(this.Jumping);
            this.WriteBool(this.Sneaking);
        }

        public override void Decode()
        {
            base.Decode();

            this.MotionX = this.ReadLFloat();
            this.MotionY = this.ReadLFloat();
            this.Jumping = this.ReadBool();
            this.Sneaking = this.ReadBool();
        }
    }
}
