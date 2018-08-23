namespace MineNET.Network.MinecraftPackets
{
    public class ModalFormResponsePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.MODAL_FORM_RESPONSE_PACKET;

        public int FormId { get; set; }
        public string FormData { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.FormId = this.ReadVarInt();
            this.FormData = this.ReadString();
        }
    }
}
