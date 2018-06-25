namespace MineNET.Entities.Metadata
{
    public class EntityDataFloat : EntityDataProperty<float>
    {
        public EntityDataFloat(int id, float data) : base(id, data)
        {

        }

        public override EntityMetadataType Type
        {
            get
            {
                return EntityMetadataType.DATA_TYPE_FLOAT;
            }
        }
    }
}
