using MineNET.Items;

namespace MineNET.Network.Packets
{
    public class NetworkInventoryAction
    {
        public const int SOURCE_CONTAINER = 0;

        public const int SOURCE_WORLD = 2;
        public const int SOURCE_CREATIVE = 3;
        public const int SOURCE_TODO = 99999;

        public const int SOURCE_TYPE_CRAFTING_ADD_INGREDIENT = -2;
        public const int SOURCE_TYPE_CRAFTING_REMOVE_INGREDIENT = -3;
        public const int SOURCE_TYPE_CRAFTING_RESULT = -4;
        public const int SOURCE_TYPE_CRAFTING_USE_INGREDIENT = -5;

        public const int SOURCE_TYPE_ANVIL_INPUT = -10;
        public const int SOURCE_TYPE_ANVIL_MATERIAL = -11;
        public const int SOURCE_TYPE_ANVIL_RESULT = -12;
        public const int SOURCE_TYPE_ANVIL_OUTPUT = -13;

        public const int SOURCE_TYPE_ENCHANT_INPUT = -15;
        public const int SOURCE_TYPE_ENCHANT_MATERIAL = -16;
        public const int SOURCE_TYPE_ENCHANT_OUTPUT = -17;

        public const int SOURCE_TYPE_TRADING_INPUT_1 = -20;
        public const int SOURCE_TYPE_TRADING_INPUT_2 = -21;
        public const int SOURCE_TYPE_TRADING_USE_INPUTS = -22;
        public const int SOURCE_TYPE_TRADING_OUTPUT = -23;

        public const int SOURCE_TYPE_BEACON = -24;

        public const int SOURCE_TYPE_CONTAINER_DROP_CONTENTS = -100;

        public const int ACTION_MAGIC_SLOT_CREATIVE_DELETE_ITEM = 0;
        public const int ACTION_MAGIC_SLOT_CREATIVE_CREATE_ITEM = 1;

        public const int ACTION_MAGIC_SLOT_DROP_ITEM = 0;
        public const int ACTION_MAGIC_SLOT_PICKUP_ITEM = 1;

        public int SourceType { get; set; }
        public int WindowId { get; set; }
        public long Unknown { get; set; }
        public int InventorySlot { get; set; }
        public Item OldItem { get; set; }
        public Item NewItem { get; set; }

        public NetworkInventoryAction(InventoryTransactionPacket pk)
        {
            this.SourceType = (int) pk.ReadUVarInt();

            if (this.SourceType == NetworkInventoryAction.SOURCE_CONTAINER)
            {
                this.WindowId = pk.ReadVarInt();
            }
            else if (this.SourceType == NetworkInventoryAction.SOURCE_WORLD)
            {
                this.Unknown = pk.ReadUVarInt();
            }
            else if (this.SourceType == NetworkInventoryAction.SOURCE_CREATIVE)
            {

            }
            else if (this.SourceType == NetworkInventoryAction.SOURCE_TODO)
            {
                this.WindowId = pk.ReadVarInt();
            }

            this.InventorySlot = (int) pk.ReadUVarInt();
            this.OldItem = pk.ReadItem();
            this.NewItem = pk.ReadItem();
        }
    }
}
