using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class BlockPickRequestPacket : DataPacket
    {
        public const int ID = ProtocolInfo.BLOCK_PICK_REQUEST_PACKET;

        public override byte PacketID
        {
            get
            {
                return BlockPickRequestPacket.ID;
            }
        }

        public Vector3i Position { get; set; }
        public bool AddUserData { get; set; }
        public byte HotbarSlot { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.Position = (Vector3i) this.ReadSBlockVector3();
            this.AddUserData = this.ReadBool();
            this.HotbarSlot = this.ReadByte();
        }
    }
}
