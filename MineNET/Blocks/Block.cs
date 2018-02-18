using MineNET.Utils;
using System;

namespace MineNET.Blocks
{
    public abstract class Block : ICloneable<Block>
    {

        public static Block Get(byte id, short meta = 0)
        {
            Block block = BlockFactory.GetBlock(id);
            block.Damage = meta;

            return block;
        }

        public static Block Get(string name)
        {
            string[] data = name.Replace("minecraft:", "").Replace(" ", "_").ToUpper().Split(':');
            int id = 0;
            int meta = 0;
            try
            {
                id = int.Parse(data[0]);
            }
            catch
            {

            }
            try
            {
                BlockFactory factory = new BlockFactory();
                id = (int) factory.GetType().GetField(data[0]).GetValue(factory);
            }
            catch
            {

            }
            try
            {
                meta = int.Parse(data[1]);
            }
            catch
            {

            }
            Block block = Block.Get((byte) id, (short) meta);
            return block;
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
