using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public abstract class ItemFood : Item
    {
        public override bool CanBeConsumed { get; } = true;
    }
}
