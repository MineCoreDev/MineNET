using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public abstract class ItemTool : Item
    {
        public ItemTool(int id) : base(id)
        {

        }

        public override bool IsTool
        {
            get
            {
                return true;
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                return 1;
            }
        }
    }
}
