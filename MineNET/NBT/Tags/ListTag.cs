using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.NBT.Tags
{
    public class ListTag<T> where T : Tag
    {
        private List<T> list;

        public ListTag(string name)
        {

        }
    }
}
