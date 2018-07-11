using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class BlockPickRequestPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.BLOCK_PICK_REQUEST_PACKET;

        public BlockCoordinate3D Position { get; set; }
        public bool AddUserData { get; set; }
        public byte HotbarSlot { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.Position = this.ReadSBlockVector3();
            this.AddUserData = this.ReadBool();
            this.HotbarSlot = this.ReadByte();
        }
    }
}
