using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Extensions
{
    public static class StringExtension
    {
        public static bool ToBool(this string str)
        {
            bool result = false;
            if (bool.TryParse(str, out result))
            {
                return result;
            }
            else return false;
        }
    }
}
