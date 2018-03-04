using System;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Blocks
{
    public abstract class Block : ICloneable<Block>, IPosition
    {

        public static Block Get(int id, int meta = 0)
        {
            Block block = BlockFactory.GetBlock(id);
            block.Damage = meta;

            return block;
        }

        public static Block Get(string name)
        {
            return BlockFactory.GetBlock(name);
        }

        public Block Clone()
        {
            return (Block) Clone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public World World { get; set; }

        public Block(int id, int meta = 0)
        {
            this.ID = id;
            this.Damage = meta;
        }

        public abstract string Name
        {
            get;
        }

        public int ID { get; }

        public int Damage { get; set; }

        public virtual byte MaxStackSize
        {
            get
            {
                return 64;
            }
        }

        public virtual bool IsTransparent
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsSolid
        {
            get
            {
                return false;
            }
        }
    }
}
