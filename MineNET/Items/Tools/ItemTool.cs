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


        public virtual bool IsSword { get; } = false;

        public virtual bool IsShovel { get; } = false;

        public virtual bool IsPickaxe { get; } = false;

        public virtual bool IsAxe { get; } = false;

        public virtual bool IsHoe { get; } = false;

        public virtual bool IsBow { get; } = false;

        public virtual bool IsShears { get; } = false;

        public virtual bool IsFlintAndSteel { get; } = false;
    }
}
