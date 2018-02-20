namespace MineNET.Values
{
    public interface ILocation : IPosition
    {
        float Yaw
        {
            get;
            set;
        }

        float Pitch
        {
            get;
            set;
        }
    }
}
