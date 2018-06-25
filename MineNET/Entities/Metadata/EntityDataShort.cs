namespace MineNET.Entities.Metadata
{
    public class EntityDataShort : EntityDataProperty<short>
    {
        public EntityDataShort(int id, short data) : base(id, data)
        {

        }

        public override EntityMetadataType Type
        {
            get
            {
                return EntityMetadataType.DATA_TYPE_SHORT;
            }
        }
    }
}
