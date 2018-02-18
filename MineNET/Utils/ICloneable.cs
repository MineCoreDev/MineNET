using System;

namespace MineNET.Utils
{
    public interface ICloneable<T> : ICloneable
    {
        new T Clone();
    }
}
