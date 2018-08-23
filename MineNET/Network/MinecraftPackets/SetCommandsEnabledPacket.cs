namespace MineNET.Network.MinecraftPackets
{
    public class SetCommandsEnabledPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_COMMANDS_ENABLED_PACKET;

        public bool Enabled { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteBool(this.Enabled);
        }
    }
}
