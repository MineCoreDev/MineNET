using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockIronDoor : BlockDoorBase
    {
        public BlockIronDoor() : base(BlockFactory.IRON_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "IronDoor";
            }
        }
    }
}
