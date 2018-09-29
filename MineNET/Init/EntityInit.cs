using System;
using MineNET.Entities;
using MineNET.Entities.Items;

namespace MineNET.Init
{
    public sealed class EntityInit : IDisposable
    {
        public static EntityInit In { get; private set; }

        public EntityInit()
        {
            EntityInit.In = this;
            this.Init();
        }

        public void Init()
        {
            this.Add(new string[] { "Item", "minecraft:item" }, new EntityItem(null, null));
        }

        public void Add(string[] keys, Entity entity)
        {
            for (int i = 0; i < keys.Length; ++i)
            {
                //MineNET_Registries.Entity.Add(keys[i], entity);
            }
        }

        public void Dispose()
        {
            EntityInit.In = null;
        }
    }
}
