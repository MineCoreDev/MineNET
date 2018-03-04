using MineNET.Entities.Data;

namespace MineNET.Entities.Metadata
{
    public abstract class EntityData
    {
        public abstract EntityMetadataType Type { get; }

        public int ID { get; set; }

        public EntityData(int id)
        {
            this.ID = id;
        }
    }
}
