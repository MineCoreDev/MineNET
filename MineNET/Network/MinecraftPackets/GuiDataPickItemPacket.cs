namespace MineNET.Network.MinecraftPackets
{
    public class GuiDataPickItemPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.GUI_DATA_PICK_ITEM_PACKET;

        public uint HotbarSlot { get; set; }

        protected override void EncodePayload()
        {

        }

        protected override void DecodePayload()
        {
            this.HotbarSlot = this.ReadLInt();
        }
    }
}
