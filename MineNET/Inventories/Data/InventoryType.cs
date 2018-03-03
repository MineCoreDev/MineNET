namespace MineNET.Inventories.Data
{
    public enum InventoryType
    {
        CONTAINER = 0,
        WORKBENCH = 1,
        FURNACE = 2,
        ENCHANTMENT = 3,
        BREWING_STAND = 4,
        DISPENSER = 6,
        DROPPER = 7,
        HOPPER = 8,
        CAULDRON = 9,
        MINECART_CHEST = 10,
        MINECART_HOPPER = 11,
        HORSE = 12,
        BEACON = 13,
        STRUCTURE_EDITOR = 14,
        TRADING = 15,
        COMMAND_BLOCK = 16,
        JUKEBOX = 17
    }

    public static class TinventoryTypeExtensions
    {
        public static byte GetIndex(this InventoryType type)
        {
            return (byte) type;
        }
    }
}
