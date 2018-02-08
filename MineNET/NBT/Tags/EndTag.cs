using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class EndTag : Tag
    {
        public EndTag() : base(null)
        {

        }

        public override byte TagID
        {
            get
            {
                return TAG_END;
            }
        }

        public override string ToString()
        {
            return "EndTag";
        }
    }
}
