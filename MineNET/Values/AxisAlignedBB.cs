namespace MineNET.Values
{
    public struct AxisAlignedBB
    {
        public Vector3 Position { get; set; }
        public Vector3 Size { get; set; }

        public AxisAlignedBB(Vector3 pos, Vector3 size)
        {
            this.Position = pos;
            this.Size = size;
        }

        public AxisAlignedBB(float posX, float posY, float posZ, float sizeX, float sizeY, float sizeZ)
        {
            this.Position = new Vector3(posX, posY, posZ);
            this.Size = new Vector3(sizeX, sizeY, sizeZ);
        }

        public bool ContainsVector(Vector3 pos)
        {
            Vector3 add = this.Position + this.Size;
            if (this.Position.X <= pos.X && add.X >= pos.X)
            {
                if (this.Position.Y <= pos.Y && add.Y >= pos.Y)
                {
                    if (this.Position.Z <= pos.Z && add.Z >= pos.Z)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Vector3 Center
        {
            get
            {
                Vector3 center = this.Size;
                center /= 2;
                return center;
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
