namespace MineNET.Values
{
    public struct AxisAlignedBB
    {
        public Vector3 Max { get; set; }
        public Vector3 Min { get; set; }

        public AxisAlignedBB(Vector3 max, Vector3 min)
        {
            this.Min = min;
            this.Max = max;
        }

        public AxisAlignedBB(float maxX, float maxY, float maxZ, float minX, float minY, float minZ)
        {
            this.Min = new Vector3(minX, minY, minZ);
            this.Max = new Vector3(maxX, maxY, maxZ);
        }

        public Vector3 Center
        {
            get
            {
                Vector3 center = this.Max - this.Min;
                center /= 2;
                return center;
            }
        }

        public Vector3 Size
        {
            get
            {
                Vector3 size = this.Max - this.Min;
                return size;
            }
        }

        public static AxisAlignedBB None
        {
            get
            {
                return new AxisAlignedBB(Vector3.Zero, Vector3.Zero);
            }
        }
    }
}
