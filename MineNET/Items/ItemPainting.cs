using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemPainting : Item
    {
        public ItemPainting() : base(ItemFactory.PAINTING)
        {

        }

        public override string Name
        {
            get
            {
                return "Painting";
            }
        }
    }
}
