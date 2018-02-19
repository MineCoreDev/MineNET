using System;
using MineNET.Utils;

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

            if (data.Length == 1)
            {
                int.TryParse(data[0], out id);
            }

            if (data.Length == 2)
            {
                int.TryParse(data[0], out id);
                int.TryParse(data[1], out meta);
            }

            id = id > 255 ? 0 : id;

            try
            {
                BlockFactory factory = new BlockFactory();
                id = (int) factory.GetType().GetField(data[0]).GetValue(factory);
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
