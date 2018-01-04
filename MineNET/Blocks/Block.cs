using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public abstract class Block : ICloneable
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        private string name = "";

        private byte id = 0;
        private short meta = 0;
        private byte count = 1;

        private bool isTransparent = false;

        public Block(byte id)
        {
            this.id = id;
        }

        public Block(byte id, short meta) : this(id)
        {
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
                return id;
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

        public virtual bool IsTransparent
        {
            get
            {
                return false;
            }
        }

        public virtual byte MaxStackSize
        {
            get
            {
                return 64;
            }
        }
    }
}
