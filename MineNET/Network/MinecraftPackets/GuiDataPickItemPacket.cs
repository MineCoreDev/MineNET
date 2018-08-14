namespace MineNET.Network.MinecraftPackets
{
    public class GuiDataPickItemPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.GUI_DATA_PICK_ITEM_PACKET;

        public uint HotbarSlot { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.HotbarSlot = this.ReadLInt();
        }
    }
}
