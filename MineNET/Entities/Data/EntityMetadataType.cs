namespace MineNET.Entities.Data
{
    public enum EntityMetadataType
    {
        DATA_TYPE_BYTE = 0,
        DATA_TYPE_SHORT = 1,
        DATA_TYPE_INT = 2,
        DATA_TYPE_FLOAT = 3,
        DATA_TYPE_STRING = 4,
        DATA_TYPE_SLOT = 5,
        DATA_TYPE_POS = 6,
        DATA_TYPE_LONG = 7,
        DATA_TYPE_VECTOR = 8,
    }

    public static class EntityMetadataTypeExtensions
    {
        public static int GetIndex(this EntityMetadataType type)
        {
            return (int) type;
        }
    }
}
