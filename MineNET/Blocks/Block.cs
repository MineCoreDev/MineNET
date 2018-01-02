using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class Block
    {
        public static Block Get(int id)
        {
            //TODO パフォーマンスの改善
            Type type = BlockFactory.blockFactory[id];
            return (Block)Activator.CreateInstance(type);
        }

        public static Block Get(int id, short meta)
        {
            Block block = Get(id);
            block.DamageID = meta;

            return block;
        }

        public static Block Get(int id, short meta, byte count)
        {
            Block block = Get(id, meta);
            block.count = count;

            return block;
        }

        public static Block Get(string name)
        {
            throw new NotImplementedException();
        }

        private string name = "";

        private int id = -1;
        private short meta = 0;
        private byte count = 1;

        private bool isTransparent = false;

        public Block(int id)
        {
            this.BlockID = id;
        }

        public string Name
        {
            get
            {
                return name;
            }

            protected set
            {
                name = value;
            }
        }

        public int BlockID
        {
            get
            {
                return id;
            }

            protected set
            {
                id = value;
            }
        }

        public short DamageID
        {
            get
            {
                return meta;
            }

            protected set
            {
                meta = value;
            }
        }

        public byte Count
        {
            get
            {
                return count;
            }

            protected set
            {
                count = value;
            }
        }

        public bool IsTransparent
        {
            get
            {
                return isTransparent;
            }

            protected set
            {
                isTransparent = value;
            }
        }
    }
}
