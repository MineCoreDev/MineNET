namespace MineNET.Network.MinecraftPackets
{
    public class TextPacket : MinecraftPacket
    {
        public const byte TYPE_RAW = 0;
        public const byte TYPE_CHAT = 1;
        public const byte TYPE_TRANSLATION = 2;
        public const byte TYPE_POPUP = 3;
        public const byte TYPE_JUKEBOX_POPUP = 4;
        public const byte TYPE_TIP = 5;
        public const byte TYPE_SYSTEM = 6;
        public const byte TYPE_WHISPER = 7;
        public const byte TYPE_ANNOUNCEMENT = 8;

        public override byte PacketID { get; } = MinecraftProtocol.TEXT_PACKET;

        public byte Type { get; set; }
        public bool NeedsTranslation { get; set; } = false;
        public string SourceName { get; set; } = "";
        public string SourceThirdPartyName { get; set; } = "";
        public int SourcePlatForm { get; set; } = 0;
        public string Message { get; set; }
        public string[] Parameters { get; set; }
        public string XboxUserId { get; set; } = "";
        public string PlatformChatId { get; set; } = "";

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.Type);
            this.WriteBool(this.NeedsTranslation);
            switch (this.Type)
            {
                case TextPacket.TYPE_CHAT:
                case TextPacket.TYPE_WHISPER:
                case TextPacket.TYPE_ANNOUNCEMENT:
                    this.WriteString(this.SourceName);
                    this.WriteString(this.SourceThirdPartyName);
                    this.WriteSVarInt(this.SourcePlatForm);
                    this.WriteString(this.Message);
                    break;

                case TextPacket.TYPE_RAW:
                case TextPacket.TYPE_TIP:
                case TextPacket.TYPE_SYSTEM:
                    this.WriteString(this.Message);
                    break;

                case TextPacket.TYPE_TRANSLATION:
                case TextPacket.TYPE_POPUP:
                case TextPacket.TYPE_JUKEBOX_POPUP:
                    this.WriteString(this.Message);
                    this.WriteUVarInt((uint) this.Parameters.Length);
                    for (int i = 0; i < this.Parameters.Length; ++i)
                    {
                        this.WriteString(this.Parameters[i]);
                    }
                    break;
            }

            this.WriteString(this.XboxUserId);
            this.WriteString(this.PlatformChatId);
        }

        public override void Decode()
        {
            base.Decode();

            this.Type = this.ReadByte();
            this.NeedsTranslation = this.ReadBool();
            switch (this.Type)
            {
                case TextPacket.TYPE_CHAT:
                case TextPacket.TYPE_WHISPER:
                case TextPacket.TYPE_ANNOUNCEMENT:
                    this.SourceName = this.ReadString();
                    this.SourceThirdPartyName = this.ReadString();
                    this.SourcePlatForm = this.ReadVarInt();
                    this.Message = this.ReadString();
                    break;

                case TextPacket.TYPE_RAW:
                case TextPacket.TYPE_TIP:
                case TextPacket.TYPE_SYSTEM:
                    this.Message = this.ReadString();
                    break;

                case TextPacket.TYPE_TRANSLATION:
                case TextPacket.TYPE_POPUP:
                case TextPacket.TYPE_JUKEBOX_POPUP:
                    this.Message = this.ReadString();
                    this.Parameters = new string[this.ReadUVarInt()];
                    for (int i = 0; i < this.Parameters.Length; ++i)
                    {
                        this.Parameters[i] = this.ReadString();
                    }
                    break;
            }

            this.XboxUserId = this.ReadString();
            this.PlatformChatId = this.ReadString();
        }
    }
}
