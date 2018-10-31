namespace MineNET.Network.MinecraftPackets
{
    public class StructureBlockUpdatePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.STRUCTURE_BLOCK_UPDATE_PACKET;

        protected override void EncodePayload()
        {

        }

        protected override void DecodePayload()
        {

        }
    }
}
