using System;
using MineNET.Entities;

namespace MineNET.Events.EntityEvents
{
    public abstract class EntityEventArgs : EventArgs
    {
        public Entity Entity { get; }

        public EntityEventArgs(Entity entity)
        {
            this.Entity = entity;
        }
    }
}
