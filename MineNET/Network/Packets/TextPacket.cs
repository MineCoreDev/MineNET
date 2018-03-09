namespace MineNET.Network.Packets
{
    public class TextPacket : DataPacket
    {
        public const int ID = ProtocolInfo.TEXT_PACKET;

        public override byte PacketID
        {
            get
            {
                return TextPacket.ID;
            }
        }

        public const byte TYPE_RAW = 0;
        public const byte TYPE_CHAT = 1;
        public const byte TYPE_TRANSLATION = 2;
        public const byte TYPE_POPUP = 3;
        public const byte TYPE_JUKEBOX_POPUP = 4;
        public const byte TYPE_TIP = 5;
        public const byte TYPE_SYSTEM = 6;
        public const byte TYPE_WHISPER = 7;
        public const byte TYPE_ANNOUNCEMENT = 8;

        public byte Type { get; set; }
        public string Source { get; set; } = "";
        public string Message { get; set; } = "";
        public string[] Parameters { get; set; } = new string[0];
        public bool IsLocalized { get; set; } = false;
        public string XboxUserId { get; set; } = "";

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.Type);
            this.WriteBool(this.IsLocalized);
            if (this.Type == TextPacket.TYPE_POPUP || this.Type == TextPacket.TYPE_CHAT || this.Type == TextPacket.TYPE_WHISPER || this.Type == TextPacket.TYPE_ANNOUNCEMENT)
            {
                this.WriteString(this.Source);
                this.WriteString(this.Message);
            }
            else if (this.Type == TextPacket.TYPE_RAW || this.Type == TextPacket.TYPE_TIP || this.Type == TextPacket.TYPE_SYSTEM)
            {
                this.WriteString(this.Message);
            }
            else if (this.Type == TextPacket.TYPE_TRANSLATION)
            {
                this.WriteString(this.Message);
                this.WriteUVarInt((uint) this.Parameters.Length);
                for (int i = 0; i < this.Parameters.Length; ++i)
                {
                    this.WriteString(this.Parameters[i]);
                }
            }
            this.WriteString(this.XboxUserId);
        }

        public override void Decode()
        {
            base.Decode();

            this.Type = this.ReadByte();
            this.IsLocalized = this.ReadBool();
            if (this.Type == TextPacket.TYPE_POPUP || this.Type == TextPacket.TYPE_CHAT || this.Type == TextPacket.TYPE_WHISPER || this.Type == TextPacket.TYPE_ANNOUNCEMENT)
            {
                this.Source = this.ReadString();
                this.Message = this.ReadString();
            }
            else if (this.Type == TextPacket.TYPE_RAW || this.Type == TextPacket.TYPE_TIP || this.Type == TextPacket.TYPE_SYSTEM)
            {
                this.Message = this.ReadString();
            }
            else if (this.Type == TextPacket.TYPE_TRANSLATION)
            {
                this.Message = this.ReadString();
                int count = (int) this.ReadUVarInt();
                this.Parameters = new string[count];
                for (int i = 0; i < count; ++i)
                {
                    this.Parameters[i] = this.ReadString();
                }
            }
            this.XboxUserId = this.ReadString();
        }
    }
}
