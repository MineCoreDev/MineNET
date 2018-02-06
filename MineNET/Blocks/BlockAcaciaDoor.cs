using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockAcaciaDoor : BlockDoorBase
    {
        public BlockAcaciaDoor() : base(BlockFactory.ACACIA_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "AcaciaDoor";
            }
        }
    }
}
