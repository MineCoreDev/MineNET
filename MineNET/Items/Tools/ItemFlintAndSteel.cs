using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemFlintAndSteel : ItemTool
    {
        public override int ID { get; } = ItemIDs.FLINT_AND_STEEL;

        public override string GetName(int damage)
        {
            return "Flint And Steel";
        }

        public override bool IsFlintAndSteel { get; } = true;
    }
}
