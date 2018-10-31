using MineNET.Data;
using MineNET.Inventories.Transactions.Data;

namespace MineNET.Network.MinecraftPackets
{
    public class InventoryTransactionPacket : MinecraftPacket
    {
        public const int TYPE_NORMAL = 0;
        public const int TYPE_MISMATCH = 1;
        public const int TYPE_USE_ITEM = 2;
        public const int TYPE_USE_ITEM_ON_ENTITY = 3;
        public const int TYPE_RELEASE_ITEM = 4;

        public const int USE_ITEM_ACTION_CLICK_BLOCK = 0;
        public const int USE_ITEM_ACTION_CLICK_AIR = 1;
        public const int USE_ITEM_ACTION_BREAK_BLOCK = 2;

        public const int RELEASE_ITEM_ACTION_RELEASE = 0; //bow shoot
        public const int RELEASE_ITEM_ACTION_CONSUME = 1; //eat food, drink potion

        public const int USE_ITEM_ON_ENTITY_ACTION_INTERACT = 0;
        public const int USE_ITEM_ON_ENTITY_ACTION_ATTACK = 1;

        public override byte PacketID { get; } = MinecraftProtocol.INVENTORY_TRANSACTION_PACKET;

        public int TransactionType { get; set; }
        public NetworkInventoryAction[] Actions { get; set; }
        public TransactionData TransactionData { get; set; }

        protected override void EncodePayload()
        {

        }

        protected override void DecodePayload()
        {
            this.TransactionType = (int) this.ReadUVarInt();

            this.Actions = new NetworkInventoryAction[this.ReadUVarInt()];
            for (int i = 0; i < this.Actions.Length; ++i)
            {
                this.Actions[i] = new NetworkInventoryAction(this);
            }

            if (this.TransactionType == InventoryTransactionPacket.TYPE_NORMAL || this.TransactionType == InventoryTransactionPacket.TYPE_MISMATCH)
            {

            }
            else if (this.TransactionType == InventoryTransactionPacket.TYPE_USE_ITEM)
            {
                this.TransactionData = new UseItemData(this);
            }
            else if (this.TransactionType == InventoryTransactionPacket.TYPE_USE_ITEM_ON_ENTITY)
            {
                this.TransactionData = new UseItemOnEntityData(this);
            }
            else if (this.TransactionType == InventoryTransactionPacket.TYPE_RELEASE_ITEM)
            {
                this.TransactionData = new ReleaseItemData(this);
            }
        }
    }
}
