using MineNET.Values;

namespace MineNET.Entities.Metadata
{
    public class EntityDataPos : EntityDataProperty<BlockCoordinate3D>
    {
        public EntityDataPos(int id, BlockCoordinate3D data) : base(id, data)
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
