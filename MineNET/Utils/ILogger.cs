using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Utils
{
    public interface ILogger
    {
        void Log(string message);
        void Log(Exception message);
        void Log(object message);

        void Warning(string message);
        void Warning(Exception message);
        void Warning(object message);

        void Error(string message);
        void Error(Exception message);
        void Error(object message);

        void Debug(string message);
        void Debug(Exception message);
        void Debug(object message);

        void Trace(Exception message);
        void Trace(object message);
    }
}
