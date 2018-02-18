using MineNET.NBT.Tags;

namespace MineNET.Entities
{
    public abstract class Entity
    {
        private static long nextEntityId = 0;

        protected long id;

        public CompoundTag namedTag;

        public Entity()
        {
            this.id = Entity.nextEntityId++;
        }

        public long GetId
        {
            get
            {
                return this.id;
            }
        }
    }
}
