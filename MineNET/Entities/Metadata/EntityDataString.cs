using System;

namespace MineNET.Entities.Metadata
{
    public class EntityDataString : EntityDataProperty<String>
    {
        public EntityDataString(int id, string data) : base(id, data)
        {

        }

        public override EntityMetadataType Type
        {
            get
            {
                return EntityMetadataType.DATA_TYPE_STRING;
            }
        }
    }
}
