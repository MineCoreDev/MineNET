using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public abstract class ItemPickaxe : ItemTool
    {
        public override ItemToolType ToolType { get; } = ItemToolType.PICKAXE;
    }
}
