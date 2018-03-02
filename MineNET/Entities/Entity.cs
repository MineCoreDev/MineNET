using MineNET.NBT.Tags;
using MineNET.Values;
using MineNET.Worlds;
using System.Collections.Generic;

namespace MineNET.Entities
{
    public abstract class Entity : ILocation
    {
        private static long nextEntityId = 0;

        protected long id;

        private List<Player> viewers = new List<Player>();

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

        public List<Player> GetViewers()
        {
            return this.viewers;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float Yaw { get; set; }
        public float Pitch { get; set; }

        public World World { get; set; }
    }
}
