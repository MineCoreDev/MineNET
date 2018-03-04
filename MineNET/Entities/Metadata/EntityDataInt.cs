using MineNET.Entities.Data;

namespace MineNET.Entities.Metadata
{
    public class EntityDataInt : EntityDataProperty<int>
    {
        public EntityDataInt(int id, int data) : base(id, data)
        {

        }

        public override EntityMetadataType Type
        {
            get
            {
                return EntityMetadataType.DATA_TYPE_INT;
            }
        }
    }
}
