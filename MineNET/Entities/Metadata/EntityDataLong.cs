using MineNET.Entities.Data;

namespace MineNET.Entities.Metadata
{
    public class EntityDataLong : EntityDataProperty<long>
    {
        public EntityDataLong(int id, long data) : base(id, data)
        {

        }

        public override EntityMetadataType Type
        {
            get
            {
                return EntityMetadataType.DATA_TYPE_LONG;
            }
        }
    }
}
