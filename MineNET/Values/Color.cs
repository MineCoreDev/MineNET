namespace MineNET.Values
{
    public struct Color
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public Color(byte r, byte g, byte b) : this(r, g, b, (byte) 0xff)
        {
        }

        public Color(byte r, byte g, byte b, byte a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public Color(int r, int g, int b) : this(r, g, b, 0xff)
        {
        }

        public Color(int r, int g, int b, int a)
        {
            this.R = (byte) (r & 0xff);
            this.G = (byte) (g & 0xff);
            this.B = (byte) (b & 0xff);
            this.A = (byte) (a & 0xff);
        }

        public int RGB
        {
            get
            {
                return (this.R << 16) | (this.G << 8) | this.B;
            }

            set
            {
                this.R = (byte) (value & 0xff >> 16);
                this.G = (byte) (value & 0xff >> 8);
                this.B = (byte) (value & 0xff);
            }
        }

        public int BGR
        {
            get
            {
                return (this.B << 16) | (this.G << 8) | this.R;
            }

            set
            {
                this.R = (byte) (value & 0xff);
                this.G = (byte) (value & 0xff >> 8);
                this.B = (byte) (value & 0xff >> 16);
            }
        }

        public int RGBA
        {
            get
            {
                return (this.R << 24) | (this.G << 16) | (this.B << 8) | this.A;
            }

            set
            {
                this.R = (byte) (value & 0xff >> 24);
                this.G = (byte) (value & 0xff >> 16);
                this.B = (byte) (value & 0xff >> 8);
                this.A = (byte) (value & 0xff);
            }
        }

        public int ABGR
        {
            get
            {
                return (this.A << 24) | (this.B << 16) | (this.G << 8) | this.R;
            }

            set
            {
                this.R = (byte) (value & 0xff);
                this.G = (byte) (value & 0xff >> 8);
                this.B = (byte) (value & 0xff >> 16);
                this.A = (byte) (value & 0xff >> 24);
            }
        }

        public int ARGB
        {
            get
            {
                return (this.A << 24) | (this.R << 16) | (this.G << 8) | this.B;
            }

            set
            {
                this.A = (byte) (value & 0xff >> 24);
                this.R = (byte) (value & 0xff >> 16);
                this.G = (byte) (value & 0xff >> 8);
                this.B = (byte) (value & 0xff);
            }
        }

        public int BGRA
        {
            get
            {
                return (this.B << 24) | (this.G << 16) | (this.R << 8) | this.A;
            }

            set
            {
                this.A = (byte) (value & 0xff);
                this.R = (byte) (value & 0xff >> 8);
                this.G = (byte) (value & 0xff >> 16);
                this.B = (byte) (value & 0xff >> 24);
            }
        }

        public override int GetHashCode()
        {
            return this.R ^ this.G << 2 ^ this.B >> 2 ^ this.A;
        }

        public override string ToString()
        {
            return $"Color: R = {this.R}, G = {this.G}, B = {this.B}, A = {this.A}";
        }

        public static Color White
        {
            get
            {
                return new Color(0xff, 0xff, 0xff);
            }
        }

        public static Color Orange
        {
            get
            {
                return new Color(0xd8, 0x7f, 0x33);
            }
        }

        public static Color Magenta
        {
            get
            {
                return new Color(0xb2, 0x4c, 0xd8);
            }
        }

        public static Color LightBlue
        {
            get
            {
                return new Color(0x66, 0x99, 0xd8);
            }
        }

        public static Color Yellow
        {
            get
            {
                return new Color(0xe5, 0xe5, 0x33);
            }
        }

        public static Color Lime
        {
            get
            {
                return new Color(0x7f, 0xcc, 0x19);
            }
        }

        public static Color Pink
        {
            get
            {
                return new Color(0xf2, 0x7f, 0xa5);
            }
        }

        public static Color Gray
        {
            get
            {
                return new Color(0x4c, 0x4c, 0x4c);
            }
        }

        public static Color LightGray
        {
            get
            {
                return new Color(0x99, 0x99, 0x99);
            }
        }

        public static Color Cyan
        {
            get
            {
                return new Color(0x4c, 0x7f, 0x99);
            }
        }

        public static Color Purple
        {
            get
            {
                return new Color(0x7f, 0x3f, 0xb2);
            }
        }

        public static Color Blue
        {
            get
            {
                return new Color(0x33, 0x4c, 0xb2);
            }
        }

        public static Color Brown
        {
            get
            {
                return new Color(0x66, 0x4c, 0x33);
            }
        }

        public static Color Green
        {
            get
            {
                return new Color(0x66, 0x7f, 0x33);
            }
        }

        public static Color Red
        {
            get
            {
                return new Color(0x99, 0x33, 0x33);
            }
        }

        public static Color Black
        {
            get
            {
                return new Color(0x19, 0x19, 0x19);
            }
        }
    }

    public static class BlockColor
    {
        public static Color Transparent
        {
            get
            {
                return new Color(0x0, 0x0, 0x0, 0x0);
            }
        }

        public static Color Void
        {
            get
            {
                return BlockColor.Transparent;
            }
        }

        public static Color Air
        {
            get
            {
                return new Color(0x0, 0x0, 0x0);
            }
        }

        public static Color Grass
        {
            get
            {
                return new Color(0x7f, 0xb2, 0x38);
            }
        }

        public static Color Sand
        {
            get
            {
                return new Color(0xf1, 0xe9, 0xa3);
            }
        }

        public static Color Cloth
        {
            get
            {
                return new Color(0xa7, 0xa7, 0xa7);
            }
        }

        public static Color Tnt
        {
            get
            {
                return new Color(0xff, 0x00, 0x00);
            }
        }

        public static Color Ice
        {
            get
            {
                return new Color(0xa0, 0xa0, 0xff);
            }
        }

        public static Color Iron
        {
            get
            {
                return new Color(0xa7, 0xa7, 0xa);
            }
        }

        public static Color Foliage
        {
            get
            {
                return new Color(0x00, 0x7c, 0x00);
            }
        }

        public static Color Snow
        {
            get
            {
                return Color.White;
            }
        }

        public static Color Clay
        {
            get
            {
                return new Color(0xa4, 0xa8, 0xb8);
            }
        }

        public static Color Dirt
        {
            get
            {
                return new Color(0xb7, 0x6a, 0x2f);
            }
        }

        public static Color Stone
        {
            get
            {
                return new Color(0x70, 0x70, 0x70);
            }
        }

        public static Color Water
        {
            get
            {
                return new Color(0x40, 0x40, 0xff);
            }
        }

        public static Color Lava
        {
            get
            {
                return BlockColor.Tnt;
            }
        }

        public static Color Wood
        {
            get
            {
                return new Color(0x68, 0x53, 0x32);
            }
        }

        public static Color Quartz
        {
            get
            {
                return new Color(0xff, 0xfc, 0xf5);
            }
        }

        public static Color Adobe
        {
            get
            {
                return Color.Orange;
            }
        }

        public static Color Gold
        {
            get
            {
                return new Color(0xfa, 0xee, 0x4d);
            }
        }

        public static Color Diamond
        {
            get
            {
                return new Color(0x5c, 0xdb, 0xd5);
            }
        }

        public static Color Lapis
        {
            get
            {
                return new Color(0x4a, 0x80, 0xff);
            }
        }

        public static Color Emerald
        {
            get
            {
                return new Color(0x00, 0xd9, 0x3a);
            }
        }

        public static Color Obsidian
        {
            get
            {
                return new Color(0x15, 0x14, 0x1f);
            }
        }

        public static Color NetherRack
        {
            get
            {
                return new Color(0x70, 0x02, 0x00);
            }
        }

        public static Color RedStone
        {
            get
            {
                return BlockColor.Tnt;
            }
        }
    }
}
