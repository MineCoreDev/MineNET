using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSpruceDoor : BlockDoorBase
    {
        public BlockSpruceDoor() : base(BlockFactory.SPRUCE_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "SpruceDoor";
            }
        }
    }
}
