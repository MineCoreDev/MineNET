namespace MineNET.Network.MinecraftPackets
{
    public class ResourcePackClientResponsePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.RESOURCE_PACK_CLIENT_RESPONSE_PACKET;

        public const byte STATUS_REFUSED = 1;
        public const byte STATUS_SEND_PACKS = 2;
        public const byte STATUS_HAVE_ALL_PACKS = 3;
        public const byte STATUS_COMPLETED = 4;

        public byte ResponseStatus { get; set; }
        public string[] PackIds { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.ResponseStatus = this.ReadByte();
            this.PackIds = new string[this.ReadShort()];
            for (int i = 0; i < this.PackIds.Length; ++i)
            {
                this.PackIds[i] = this.ReadString();
            }
        }
    }
}
