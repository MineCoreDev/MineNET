using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenHelmet : ItemHelmet
    {
        public override int ID { get; } = ItemIDs.GOLDEN_HELMET;

        public override string GetName(int damage)
        {
            return "Golden Helmet";
        }

        public override int MaxDurability { get; } = 77;
    }
}
