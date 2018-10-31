using System;
using MineNET.BlockEntities;

namespace MineNET.Init
{
    public sealed class BlockEntityInit : IDisposable
    {
        public static BlockEntityInit In { get; private set; }

        public BlockEntityInit()
        {
            BlockEntityInit.In = this;
            this.Init();
        }

        public void Init()
        {
            this.Set("chest", typeof(BlockEntityChest));
        }

        public void Set(string name, Type type)
        {
            MineNET_Registries.BlockEntity[name] = type;
        }

        public void Dispose()
        {
            BlockEntityInit.In = null;
        }
    }
}
