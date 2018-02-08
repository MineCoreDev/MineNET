using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class EndTag : Tag
    {
        public new const byte ID = TAG_END;

        public EndTag() : base(null)
        {

        }

        public override string ToString()
        {
            return "EndTag";
        }
    }
}
