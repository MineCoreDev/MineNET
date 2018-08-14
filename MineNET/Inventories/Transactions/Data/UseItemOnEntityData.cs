using MineNET.Items;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;

namespace MineNET.Inventories.Transactions.Data
{
    public class UseItemOnEntityData : TransactionData
    {
        public long EntityRuntimeId { get; set; }
        public int ActionType { get; set; }
        public int HotbarSlot { get; set; }
        public ItemStack ItemMainHand { get; set; }
        public Vector3 PlayerPos { get; set; }
        public Vector3 ClickPos { get; set; }

        public UseItemOnEntityData(InventoryTransactionPacket pk)
        {
            this.EntityRuntimeId = pk.ReadEntityRuntimeId();
            this.ActionType = (int) pk.ReadUVarInt();
            this.HotbarSlot = pk.ReadSVarInt();
            this.ItemMainHand = pk.ReadItem();
            this.PlayerPos = pk.ReadVector3();
            this.ClickPos = pk.ReadVector3();
        }
    }
}
