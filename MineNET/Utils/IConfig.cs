using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Utils
{
    public interface IConfig
    {
        void Save<T>();
    }
}
