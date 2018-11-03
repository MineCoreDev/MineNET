namespace MineNET.Values
{
    public interface ILocation : IPosition
    {
        float Yaw { get; }
        float Pitch { get; }
    }

    public static class ILocationExtensions
    {
        public static Location ToLocation(this ILocation location)
        {
            return new Location(location.X, location.Y, location.Z, location.Yaw, location.Pitch, location.World);
        }
    }
}
