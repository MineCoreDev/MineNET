using MineNET.Entities.Data;
using MineNET.Items;

namespace MineNET.Entities.Metadata
{
    public class EntityDataSlot : EntityDataProperty<Item>
    {
        public EntityDataSlot(int id, Item data) : base(id, data)
        {

        }

        public override EntityMetadataType Type
        {
            get
            {
                return EntityMetadataType.DATA_TYPE_SLOT;
            }
        }
    }
}
