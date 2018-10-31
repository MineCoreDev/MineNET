namespace MineNET.Network.MinecraftPackets
{
    public class BossEventPacket : MinecraftPacket
    {
        public const int TYPE_SHOW = 0;
        public const int TYPE_REGISTER_PLAYER = 1;
        public const int TYPE_HIDE = 2;
        public const int TYPE_UNREGISTER_PLAYER = 3;
        public const int TYPE_HEALTH_PERCENT = 4;
        public const int TYPE_TITLE = 5;
        public const int TYPE_UNKNOWN_6 = 6;
        public const int TYPE_TEXTURE = 7;

        public override byte PacketID { get; } = MinecraftProtocol.BOSS_EVENT_PACKET;

        public long BossEid { get; set; }
        public uint EventType { get; set; }

        public long PlayerEid { get; set; }
        public float HealthPercent { get; set; }
        public string Title { get; set; }
        public ushort UnknownShort { get; set; }
        public uint Color { get; set; }
        public uint Overlay { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityUniqueId(this.BossEid);
            this.WriteUVarInt(this.EventType);
            switch (this.EventType)
            {
                case BossEventPacket.TYPE_REGISTER_PLAYER:
                case BossEventPacket.TYPE_UNREGISTER_PLAYER:
                    this.WriteEntityUniqueId(this.PlayerEid);
                    break;

                case BossEventPacket.TYPE_SHOW:
                    this.WriteString(this.Title);
                    this.WriteLFloat(this.HealthPercent);
                    this.WriteLShort(this.UnknownShort);
                    this.WriteUVarInt(this.Color);
                    this.WriteUVarInt(this.Overlay);
                    break;

                case BossEventPacket.TYPE_UNKNOWN_6:
                    this.WriteLShort(this.UnknownShort);
                    this.WriteUVarInt(this.Color);
                    this.WriteUVarInt(this.Overlay);
                    break;

                case BossEventPacket.TYPE_TEXTURE:
                    this.WriteUVarInt(this.Color);
                    this.WriteUVarInt(this.Overlay);
                    break;

                case BossEventPacket.TYPE_HEALTH_PERCENT:
                    this.WriteLFloat(this.HealthPercent);
                    break;

                case BossEventPacket.TYPE_TITLE:
                    this.WriteString(this.Title);
                    break;
            }
        }

        protected override void DecodePayload()
        {
            this.BossEid = this.ReadEntityUniqueId();
            this.EventType = this.ReadUVarInt();
            switch (this.EventType)
            {
                case BossEventPacket.TYPE_REGISTER_PLAYER:
                case BossEventPacket.TYPE_UNREGISTER_PLAYER:
                    this.PlayerEid = this.ReadEntityUniqueId();
                    break;

                case BossEventPacket.TYPE_SHOW:
                    this.Title = this.ReadString();
                    this.HealthPercent = this.ReadLFloat();
                    this.UnknownShort = this.ReadLShort();
                    this.Color = this.ReadUVarInt();
                    this.Overlay = this.ReadUVarInt();
                    break;

                case BossEventPacket.TYPE_UNKNOWN_6:
                    this.UnknownShort = this.ReadLShort();
                    this.Color = this.ReadUVarInt();
                    this.Overlay = this.ReadUVarInt();
                    break;

                case BossEventPacket.TYPE_TEXTURE:
                    this.Color = this.ReadUVarInt();
                    this.Overlay = this.ReadUVarInt();
                    break;

                case BossEventPacket.TYPE_HEALTH_PERCENT:
                    this.HealthPercent = this.ReadLFloat();
                    break;

                case BossEventPacket.TYPE_TITLE:
                    this.Title = this.ReadString();
                    break;
            }
        }
    }
}
