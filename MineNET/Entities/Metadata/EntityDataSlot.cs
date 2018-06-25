using MineNET.Items;

namespace MineNET.Entities.Metadata
{
    public class EntityDataSlot : EntityDataProperty<ItemStack>
    {
        public EntityDataSlot(int id, ItemStack data) : base(id, data)
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
