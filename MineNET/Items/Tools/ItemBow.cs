using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBow : ItemTool
    {
        public override int ID { get; } = ItemIDs.BOW;

        public override string GetName(int damage)
        {
            return "Bow";
        }

        public override ItemToolType ToolType { get; } = ItemToolType.BOW;

        public override int MaxDurability { get; } = 385;
    }
}
