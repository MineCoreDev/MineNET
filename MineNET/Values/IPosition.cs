using MineNET.Worlds;

namespace MineNET.Values
{
    public interface IPosition : IVector3
    {
        World World { get; }

        Vector3 GetVector3();
        Vector2 GetVector2();
    }
}
