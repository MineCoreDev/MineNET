namespace MineNET.Network.Packets
{
    public class ClientboundMapItemDataPacket : DataPacket
    {
        public const int TEXTURE_UPDATE = 0x02;
        public const int DECORATION_UPDATE = 0x04;
        public const int ENTITIES_UPDATE = ClientboundMapItemDataPacket.TEXTURE_UPDATE | ClientboundMapItemDataPacket.DECORATION_UPDATE;

        public override byte PacketID
        {
            get
            {
                return ProtocolInfo.CLIENTBOUND_MAP_ITEM_DATA_PACKET;
            }
        }

        public int MapId { get; set; }
        public int Type { get; set; }
        public int DimensionId { get; set; }

        public long[] EntityRuntimeIds { get; set; }
        public int Scale { get; set; }

        public long[] DecorationEntityUniqueIds { get; set; }
        //public Decorator Decorations { get; set;} 

        public int Width { get; set; }
        public int Height { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int[] Colors { get; set; }
    }
}
