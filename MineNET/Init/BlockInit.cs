using MineNET.Blocks;
using System;

namespace MineNET.Init
{
    public sealed class BlockInit : IDisposable
    {
        public static BlockInit In { get; private set; }

        public BlockAir Air { get; } = new BlockAir();
        public BlockStone Stone { get; } = new BlockStone();
        public BlockGrass Grass { get; } = new BlockGrass();
        public BlockDirt Dirt { get; } = new BlockDirt();

        public BlockInit()
        {
            BlockInit.In = this;
            this.Init();
        }

        public void Init()
        {
            this.Add(BlockIDs.AIR, this.Air);
            this.Add(BlockIDs.STONE, this.Stone);
            this.Add(BlockIDs.GRASS, this.Grass);
            this.Add(BlockIDs.DIRT, this.Dirt);
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
