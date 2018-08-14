using MineNET.Items;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;

namespace MineNET.Inventories.Transactions.Data
{
    public class ReleaseItemData : TransactionData
    {
        public int ActionType { get; set; }
        public int HotbarSlot { get; set; }
        public ItemStack MainHandItem { get; set; }
        public Vector3 HeadRot { get; set; }

        public ReleaseItemData(InventoryTransactionPacket pk)
        {
            this.ActionType = (int) pk.ReadUVarInt();
            this.HotbarSlot = pk.ReadSVarInt();
            this.MainHandItem = pk.ReadItem();
            this.HeadRot = pk.ReadVector3();
        }
    }
}
