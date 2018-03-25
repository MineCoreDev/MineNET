using MineNET.Worlds;

namespace MineNET.Values
{
    public interface IPosition : IVector3
    {
        World World { get; }
    }
}
