namespace MineNET.Network.MinecraftPackets
{
    public class EventPacket : MinecraftPacket
    {
        public const int TYPE_ACHIEVEMENT_AWARDED = 0;
        public const int TYPE_ENTITY_INTERACT = 1;
        public const int TYPE_PORTAL_BUILT = 2;
        public const int TYPE_PORTAL_USED = 3;
        public const int TYPE_MOB_KILLED = 4;
        public const int TYPE_CAULDRON_USED = 5;
        public const int TYPE_PLAYER_DEATH = 6;
        public const int TYPE_BOSS_KILLED = 7;
        public const int TYPE_AGENT_COMMAND = 8;
        public const int TYPE_AGENT_CREATED = 9;

        public override byte PacketID { get; } = MinecraftProtocol.EVENT_PACKET;

        public long PlayerRuntimeId { get; set; }
        public int EventData { get; set; }
        public byte Type { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.PlayerRuntimeId);
            this.WriteVarInt(this.EventData);
            this.WriteByte(this.Type);
        }

        public override void Decode()
        {
            base.Decode();

            this.PlayerRuntimeId = this.ReadEntityRuntimeId();
            this.EventData = this.ReadVarInt();
            this.Type = this.ReadByte();
        }
    }
}
