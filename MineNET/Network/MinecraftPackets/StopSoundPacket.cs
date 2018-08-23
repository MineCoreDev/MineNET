namespace MineNET.Network.MinecraftPackets
{
    public class StopSoundPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.STOP_SOUND_PACKET;

        public string SoundName { get; set; }
        public bool StopAll { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.SoundName);
            this.WriteBool(this.StopAll);
        }
    }
}
