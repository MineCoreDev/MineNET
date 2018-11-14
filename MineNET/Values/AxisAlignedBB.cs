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

        public AxisAlignedBB SetBounds(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            return new AxisAlignedBB(minX, minY, minZ, maxX, maxY, maxZ);
        }

        public AxisAlignedBB SetBB(AxisAlignedBB bb)
        {
            return this.SetBounds(bb.Position.X, bb.Position.Y, bb.Position.Z, bb.Size.X, bb.Size.Y, bb.Size.Z);
        }

        public AxisAlignedBB AddCoord(float x, float y, float z)
        {
            Vector3 pos = this.Position;
            Vector3 size = this.Size;

            if (x < 0)
            {
                pos.X += x;
            } else
            {
                size.X += x;
            }
            if (y < 0)
            {
                pos.Y += y;
            } else
            {
                size.Y += y;
            }
            if (z < 0)
            {
                pos.Z += z;
            } else
            {
                size.Z += z;
            }
            return new AxisAlignedBB(pos, size);
        }

        public AxisAlignedBB Expand(float x, float y, float z)
        {
            Vector3 pos = this.Position;
            Vector3 size = this.Size;

            return new AxisAlignedBB(pos.X - x, pos.Y - y, pos.Z - z, size.X + x, size.Y + y, size.Z + z);
        }

        public AxisAlignedBB Offset(float x, float y, float z)
        {
            Vector3 pos = this.Position;
            Vector3 size = this.Size;

            return new AxisAlignedBB(pos.X + x, pos.Y + y, pos.Z + z, size.X + x, size.Y + y, size.Z + z);
        }

        public AxisAlignedBB Contract(float x, float y, float z)
        {
            Vector3 pos = this.Position;
            Vector3 size = this.Size;

            return new AxisAlignedBB(pos.X + x, pos.Y + y, pos.Z + z, size.X - x, size.Y - y, size.Z - z);
        }

        public float CalculateXOffset(AxisAlignedBB bb, float x)
        {
            if (bb.Size.Y <= this.Position.Y || bb.Position.Y >= this.Size.Y)
            {
                return x;
            }
            if (bb.Size.Z <= this.Position.Z || bb.Position.Z >= this.Size.Z)
            {
                return x;
            }
            if (x > 0 && bb.Size.X <= this.Position.X)
            {
                float x1 = this.Position.X - bb.Size.X;
                if (x1 < x)
                {
                    x = x1;
                }
            }
            else if (x < 0 && bb.Position.X >= this.Size.X)
            {
                float x2 = this.Size.X - bb.Position.X;
                if (x2 > x)
                {
                    x = x2;
                }
            }
            return x;
        }

        public float CalculateYOffset(AxisAlignedBB bb, float y)
        {
            if (bb.Size.X <= this.Position.X || bb.Position.X >= this.Size.X)
            {
                return y;
            }
            if (bb.Size.Z <= this.Position.Z || bb.Position.Z >= this.Size.Z)
            {
                return y;
            }
            if (y > 0 && bb.Size.Y <= this.Position.Y)
            {
                float y1 = this.Position.Y - bb.Size.Y;
                if (y1 < y)
                {
                    y = y1;
                }
            }
            else if (y < 0 && bb.Position.Y >= this.Size.Y)
            {
                float y2 = this.Size.Y - bb.Position.Y;
                if (y2 > y)
                {
                    y = y2;
                }
            }
            return y;
        }

        public float CalculateZOffset(AxisAlignedBB bb, float z)
        {
            if (bb.Size.X <= this.Position.X || bb.Position.X >= this.Size.X)
            {
                return z;
            }
            if (bb.Size.Y <= this.Position.Y || bb.Position.Y >= this.Size.Y)
            {
                return z;
            }
            if (z > 0 && bb.Size.Z <= this.Position.Z)
            {
                float z1 = this.Position.Z - bb.Size.Z;
                if (z1 < z)
                {
                    z = z1;
                }
            }
            else if (z < 0 && bb.Position.Z >= this.Size.Z)
            {
                float z2 = this.Size.Z - bb.Position.Z;
                if (z2 > z)
                {
                    z = z2;
                }
            }
            return z;
        }

        public bool IntersectsWith(AxisAlignedBB bb, float epsilon = 0.00001f)
        {
            if (bb.Size.X - this.Position.X > epsilon && this.Size.X - bb.Position.X > epsilon)
            {
                if (bb.Size.Y - this.Position.Y > epsilon && this.Size.Y - bb.Position.Y > epsilon)
                {
                    return bb.Size.Z - this.Position.Z > epsilon && this.Size.Z - bb.Position.Z > epsilon;
                }
            }
            return false;
        }

        public static AxisAlignedBB None
        {
            get
            {
                return new AxisAlignedBB(Vector3.Zero, Vector3.Zero);
            }
        }

        public override string ToString()
        {
            return $"AxisAlignedBB {this.Position.X} {this.Position.Y} {this.Position.Z} {this.Size.X} {this.Size.Y} {this.Size.Z}";
        }
    }
}
