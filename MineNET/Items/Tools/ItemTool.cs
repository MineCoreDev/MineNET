using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public abstract class ItemTool : Item
    {
        public override byte MaxStackSize { get; } = 1;

        public override bool IsTool { get; } = true;

        public virtual ItemToolType ToolType { get; } = ItemToolType.NONE;

        public virtual ItemToolTier ToolTier { get; } = ItemToolTier.NONE;

        public virtual int MaxDurability { get; } = 0;
    }
}
