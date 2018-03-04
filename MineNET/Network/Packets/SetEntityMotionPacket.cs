using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class SetEntityMotionPacket : DataPacket
    {
        public const int ID = ProtocolInfo.SET_ENTITY_MOTION_PACKET;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        public long EntityRuntimeId { get; set; }

        public Vector3 Motion { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteVector3(this.Motion);
        }
    }
}
