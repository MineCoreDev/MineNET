namespace MineNET.Values
{
    public interface ILocation : IPosition
    {
        float Yaw { get; }
        float Pitch { get; }

        Vector2 GetRotateVector2();
    }
}
