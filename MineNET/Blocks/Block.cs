using System;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Blocks
{
    public abstract class Block : ICloneable<Block>, IPosition
    {

        public static Block Get(byte id, short meta = 0)
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

        private int x;
        private int y;
        private int z;

        private World world = null;

        public float X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = (int) value;
            }
        }

        public float Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = (int) value;
            }
        }

        public float Z
        {
            get
            {
                return this.z;
            }

            set
            {
                this.z = (int) value;
            }
        }
        public World World
        {
            get
            {
                return this.world;
            }

            set
            {
                this.world = value;
            }
        }

        private byte id;
        private short meta;

        public Block(byte id, short meta = 0)
        {
            this.id = id;
            this.meta = meta;
        }

        public abstract string Name
        {
            get;
        }

        public byte BlockID
        {
            get
            {
                return this.id;
            }
        }

        public short Damage
        {
            get
            {
                return this.meta;
            }

            set
            {
                this.meta = value;
            }
        }

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
