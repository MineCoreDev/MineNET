using MineNET.Entities.Data;
using MineNET.Values;

namespace MineNET.Entities.Metadata
{
    public class EntityDataVector : EntityDataProperty<Vector3>
    {
        public EntityDataVector(int id, Vector3 data) : base(id, data)
        {

        }

        public override EntityMetadataType Type
        {
            get
            {
                return EntityMetadataType.DATA_TYPE_VECTOR;
            }
        }
    }
}
