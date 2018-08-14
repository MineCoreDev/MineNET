namespace MineNET.Data
{
    public enum ContainerIds
    {
        NONE = -1,
        INVENTORY = 0,
        FIRST = 1,
        LAST = 100,
        OFFHAND = 119,
        ARMOR = 120,
        CREATIVE = 121,
        HOTBAR = 122,
        FIXED_INVENTORY = 123,
        CURSOR = 124,
    }

    public static class ContainerIdsExtensions
    {
        public static byte GetIndex(this ContainerIds ids)
        {
            return (byte) ids;
        }
    }
}
