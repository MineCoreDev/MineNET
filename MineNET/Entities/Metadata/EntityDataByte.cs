using MineNET.Entities.Data;

namespace MineNET.Entities.Metadata
{
    public class EntityDataByte : EntityDataProperty<byte>
    {
        public EntityDataByte(int id, byte data) : base(id, data)
        {

        }

        public EntityDataByte(int id, bool data) : base(id, data ? (byte) 0 : (byte) 1)
        {

        }

        public override EntityMetadataType Type
        {
            get
            {
                return EntityMetadataType.DATA_TYPE_BYTE;
            }
        }
    }
}
