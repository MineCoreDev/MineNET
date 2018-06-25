using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Utils
{
    public static class JWT
    {
        public static string Decode(string data)
        {
            string[] datas = data.Split('.');
            
            string d1 = datas[1]
                .PadRight(datas[1].Length + (4 - datas[1].Length % 4) % 4, '=')
                .Replace('_', '/')
                .Replace('-', '+');

            string result = "{}";

            try
            {
                result = Encoding.UTF8.GetString(Convert.FromBase64String(d1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return result;//return Convert.ToBase64String(Encoding.UTF8.GetBytes(d1));
        }
    }
}
