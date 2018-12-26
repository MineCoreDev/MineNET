namespace MineNET.Network.MinecraftPackets
{
    public class BiomeDefinitionListPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.BIOME_DEFINITION_LIST_PACKET;

        public byte[] Tag { get; set; }

        protected override void EncodePayload()
        {
            this.WriteBytes(this.Tag);
        }

        protected override void DecodePayload()
        {
            this.Tag = this.ReadBytes();
        }
    }
}
