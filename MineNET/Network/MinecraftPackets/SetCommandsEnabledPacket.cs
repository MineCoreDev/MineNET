namespace MineNET.Network.MinecraftPackets
{
    public class SetCommandsEnabledPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_COMMANDS_ENABLED_PACKET;

        public bool Enabled { get; set; }

        protected override void EncodePayload()
        {
            this.WriteBool(this.Enabled);
        }

        protected override void DecodePayload()
        {

        }
    }
}
