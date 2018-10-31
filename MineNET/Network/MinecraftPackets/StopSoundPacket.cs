namespace MineNET.Network.MinecraftPackets
{
    public class StopSoundPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.STOP_SOUND_PACKET;

        public string SoundName { get; set; }
        public bool StopAll { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.SoundName);
            this.WriteBool(this.StopAll);
        }

        protected override void DecodePayload()
        {

        }
    }
}
