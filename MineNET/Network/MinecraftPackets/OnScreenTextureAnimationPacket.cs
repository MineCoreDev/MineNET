namespace MineNET.Network.MinecraftPackets
{
    public class OnScreenTextureAnimationPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ON_SCREEN_TEXTURE_ANIMATION_PACKET;

        public uint EffectId { get; set; }

        protected override void EncodePayload()
        {
            this.WriteLInt(this.EffectId);
        }

        protected override void DecodePayload()
        {
            this.EffectId = this.ReadLInt();
        }
    }
}
