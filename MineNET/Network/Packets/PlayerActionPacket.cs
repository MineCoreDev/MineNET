using MineNET.Blocks.Data;
using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class PlayerActionPacket : DataPacket
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

        public const int ACTION_SET_ENCHANTMENT_SEED = 20;

        public const int ID = ProtocolInfo.PLAYER_ACTION_PACKET;

        public override byte PacketID
        {
            get
            {
                return PlayerActionPacket.ID;
            }
        }

        public long EntityRuntimeId { get; set; }
        public int ActionType { get; set; }
        public Vector3i Position { get; set; }
        public BlockFace Face { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.EntityRuntimeId = this.ReadEntityRuntimeId();
            this.ActionType = this.ReadSVarInt();
            this.Position = this.ReadBlockVector3();
            this.Face = this.ReadBlockFace();
        }
    }
}
