namespace MineNET.Values
{
    public class Vector3i : IVector3i
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector3i(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3 Vector3
        {
            get
            {
                return new Vector3(this.X, this.Y, this.Z);
            }
        }
    }
}
