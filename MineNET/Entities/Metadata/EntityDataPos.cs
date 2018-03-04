using MineNET.Entities.Data;
using MineNET.Values;

namespace MineNET.Entities.Metadata
{
    public class EntityDataPos : EntityDataProperty<Vector3i>
    {
        public EntityDataPos(int id, Vector3i data) : base(id, data)
        {

        }

        public override EntityMetadataType Type
        {
            get
            {
                return EntityMetadataType.DATA_TYPE_POS;
            }
        }
    }
}
