using MineNET.Worlds;

namespace MineNET.Values
{
    public interface IPosition : IVector3
    {
        World World { get; }
    }

    public static class IPositionExtensions
    {
        public static Position ToPosition(this IPosition position)
        {
            return new Position(position.X, position.Y, position.Z, position.World);
        }
    }
}
