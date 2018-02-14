
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDarkOakDoor : BlockDoorBase
    {
        public BlockDarkOakDoor() : base(BlockFactory.DARK_OAK_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "DarkOakDoor";
            }
        }
    }
}
