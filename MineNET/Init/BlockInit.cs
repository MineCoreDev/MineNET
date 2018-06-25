using MineNET.Blocks;
using System;

namespace MineNET.Init
{
    public sealed class BlockInit : IDisposable
    {
        public static BlockInit In { get; private set; }

        public BlockAir Air { get; } = new BlockAir();

        public BlockInit()
        {
            BlockInit.In = this;
        }

        public void Init()
        {
            this.Add(BlockIDs.AIR, this.Air);
        }

        public void Add(int id, Block block)
        {
            MineNET_Registries.Block.Add(id, block);
        }

        public void Dispose()
        {
            BlockInit.In = null;
        }
    }
}
