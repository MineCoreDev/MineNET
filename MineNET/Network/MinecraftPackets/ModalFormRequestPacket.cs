namespace MineNET.Network.MinecraftPackets
{
    public class ModalFormRequestPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.MODAL_FORM_REQUEST_PACKET;

        public int FormId { get; set; }
        public string FormData { get; set; }

        protected override void EncodePayload()
        {
            this.WriteVarInt(this.FormId);
            this.WriteString(this.FormData);
        }

        protected override void DecodePayload()
        {

        }
    }
}
