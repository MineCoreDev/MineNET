namespace MineNET.Network.Packets
{
    public class AnimatePacket : DataPacket
    {
        public const int ACTION_NO_ACTION = 0;
        public const int ACTION_SWING = 1;
        public const int ACTION_WAKE_UP = 3;
        public const int ACTION_CRITICAL_HIT = 4;
        public const int ACTION_MAGIC_CRITICAL_HIT = 5;
        public const int ACTION_ROW_RIGHT = 128;
        public const int ACTION_ROW_LEFT = 129;

        public const int ID = ProtocolInfo.ANIMATE_PACKET;

        public override byte PacketID
        {
            get
            {
                return AnimatePacket.ID;
            }
        }

        public int Action { get; set; }
        public long EntityRuntimeId { get; set; }
        public float Unknown { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteSVarInt(this.Action);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            if ((this.Action & 0x80) != 0)
            {
                this.WriteLFloat(this.Unknown);
            }
        }

        public override void Decode()
        {
            base.Decode();

            this.Action = this.ReadSVarInt();
            this.EntityRuntimeId = this.ReadEntityRuntimeId();
            if ((this.Action & 0x80) != 0)
            {
                this.Unknown = this.ReadLFloat();
            }
        }
    }
}
