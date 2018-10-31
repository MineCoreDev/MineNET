using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class SetSpawnPositionPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_SPAWN_POSITION_PACKET;

        public int SpawnType { get; set; }
        public BlockCoordinate3D Position { get; set; }
        public bool SpawnForced { get; set; }

        protected override void EncodePayload()
        {
            this.WriteVarInt(this.SpawnType);
            this.WriteBlockVector3(this.Position);
            this.WriteBool(this.SpawnForced);
        }

        protected override void DecodePayload()
        {
            this.SpawnType = this.ReadVarInt();
            this.Position = this.ReadBlockVector3();
            this.SpawnForced = this.ReadBool();
        }
    }
}
