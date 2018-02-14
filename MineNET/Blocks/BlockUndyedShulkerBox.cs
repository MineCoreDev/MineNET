using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockUndyedShulkerBox : Block
    {
        public BlockUndyedShulkerBox() : base(BlockFactory.UNDYED_SHULKER_BOX)
        {

        }

        public override string Name
        {
            get
            {
                return "UndyedShulkerBox";
            }
        }
    }
}
