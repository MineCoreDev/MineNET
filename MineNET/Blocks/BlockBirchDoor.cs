using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBirchDoor : BlockDoorBase
    {
        public BlockBirchDoor() : base(BlockFactory.BIRCH_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "BirchDoor";
            }
        }
    }
}
