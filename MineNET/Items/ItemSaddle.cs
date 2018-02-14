using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemSaddle : Item
    {
        public ItemSaddle() : base(ItemFactory.SADDLE)
        {

        }

        public override string Name
        {
            get
            {
                return "Saddle";
            }
        }
    }
}
