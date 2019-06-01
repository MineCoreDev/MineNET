namespace MineNET.Network.MinecraftPackets
{
    public class LecternUpdatePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.LECTERN_UPDATE_PACKET;

        protected override void DecodePayload()
        {
        }

        protected override void EncodePayload()
        {
        }
    }
}
