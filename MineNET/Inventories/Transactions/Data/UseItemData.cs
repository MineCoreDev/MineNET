using MineNET.Data;
using MineNET.Items;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;

namespace MineNET.Inventories.Transactions.Data
{
    public class UseItemData : TransactionData
    {
        public int ActionType { get; set; }
        public BlockCoordinate3D BlockPos { get; set; }
        public BlockFace Face { get; set; }
        public int HotbarSlot { get; set; }
        public ItemStack ItemMainHand { get; set; }
        public Vector3 PlayerPos { get; set; }
        public Vector3 ClickPos { get; set; }

        public UseItemData(InventoryTransactionPacket pk)
        {
            this.ActionType = (int) pk.ReadUVarInt();
            this.BlockPos = pk.ReadBlockVector3();
            this.Face = pk.ReadBlockFace();
            this.HotbarSlot = pk.ReadSVarInt();
            this.ItemMainHand = pk.ReadItem();
            this.PlayerPos = pk.ReadVector3();
            this.ClickPos = pk.ReadVector3();
        }
    }
}
