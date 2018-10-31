using MineNET.Data;
using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class PlayerActionPacket : MinecraftPacket
    {
        public const int ACTION_START_BREAK = 0;
        public const int ACTION_ABORT_BREAK = 1;
        public const int ACTION_STOP_BREAK = 2;
        public const int ACTION_GET_UPDATED_BLOCK = 3;
        public const int ACTION_DROP_ITEM = 4;
        public const int ACTION_START_SLEEPING = 5;
        public const int ACTION_STOP_SLEEPING = 6;
        public const int ACTION_RESPAWN = 7;
        public const int ACTION_JUMP = 8;
        public const int ACTION_START_SPRINT = 9;
        public const int ACTION_STOP_SPRINT = 10;
        public const int ACTION_START_SNEAK = 11;
        public const int ACTION_STOP_SNEAK = 12;
        public const int ACTION_DIMENSION_CHANGE_REQUEST = 13; //sent when dying in different dimension
        public const int ACTION_DIMENSION_CHANGE_ACK = 14; //sent when spawning in a different dimension to tell the server we spawned
        public const int ACTION_START_GLIDE = 15;
        public const int ACTION_STOP_GLIDE = 16;
        public const int ACTION_BUILD_DENIED = 17;
        public const int ACTION_CONTINUE_BREAK = 18;
        public const int ACTION_CHANGE_SKIN = 19;
        public const int ACTION_SET_ENCHANTMENT_SEED = 20;
        public const int ACTION_START_SWIMMING = 21;
        public const int ACTION_STOP_SWIMMING = 22;
        public const int ACTION_START_SPIN_ATTACK = 23;
        public const int ACTION_STOP_SPIN_ATTACK = 24;

        public override byte PacketID { get; } = MinecraftProtocol.PLAYER_ACTION_PACKET;

        public long EntityRuntimeId { get; set; }
        public int Action { get; set; }
        public BlockCoordinate3D Position { get; set; }
        public BlockFace Face { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteSVarInt(this.Action);
            this.WriteBlockVector3(this.Position);
            this.WriteBlockFace(this.Face);
        }

        protected override void DecodePayload()
        {
            this.EntityRuntimeId = this.ReadEntityRuntimeId();
            this.Action = this.ReadSVarInt();
            this.Position = this.ReadBlockVector3();
            this.Face = this.ReadBlockFace();
        }
    }
}
