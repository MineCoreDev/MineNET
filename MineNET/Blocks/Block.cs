using System;
using MineNET.Utils;

namespace MineNET.Blocks
{
    public abstract class Block : ICloneable<Block>
    {
        public static Block Get(byte id)
        {
            Block block = BlockFactory.GetBlock(id);
            return block;
        }

        public static Block Get(byte id, short meta)
        {
            Block block = Get(id);
            block.Damage = meta;

            return block;
        }

        public static Block Get(byte id, short meta, byte count)
        {
            Block block = Get(id, meta);
            block.Count = count;

            return block;
        }

        public static Block Get(string name)
        {
            throw new NotImplementedException();
        }

        public Block Clone()
        {
            return (Block) Clone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        private byte id;
        private short meta;
        private byte count;

        public Block(byte id) : this(id, 0)
        {

        }

        public Block(byte id, short meta) : this(id, meta, 1)
        {

        }

        public Block(byte id, short meta, byte count)
        {
            this.id = id;
            this.meta = meta;
            this.count = count;
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

        public byte Count
        {
            get
            {
                return this.count;
            }

            set
            {
                this.count = value;
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
