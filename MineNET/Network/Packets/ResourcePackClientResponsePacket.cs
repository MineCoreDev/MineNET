namespace MineNET.Network.Packets
{
    public class ResourcePackClientResponsePacket : DataPacket
    {
        public const int ID = ProtocolInfo.RESOURCE_PACK_CLIENT_RESPONSE_PACKET;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        public const byte STATUS_REFUSED = 1;
        public const byte STATUS_SEND_PACKS = 2;
        public const byte STATUS_HAVE_ALL_PACKS = 3;
        public const byte STATUS_COMPLETED = 4;

        byte responseStatus;
        public byte ResponseStatus
        {
            get
            {
                return this.responseStatus;
            }

            set
            {
                this.responseStatus = value;
            }
        }

        string[] packIds;
        public string[] PackIds
        {
            get
            {
                return this.packIds;
            }

            set
            {
                this.packIds = value;
            }
        }

        public override void Decode()
        {
            base.Decode();

            this.responseStatus = this.ReadByte();
            this.packIds = new string[this.ReadLShort()];
            for (int i = 0; i < this.packIds.Length; i++)
            {
                this.packIds[i] = this.ReadString();
            }
        }
    }
}
