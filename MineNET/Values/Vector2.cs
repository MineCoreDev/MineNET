using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Values
{
    public struct Vector2
    {
        private double x;
        private double y;

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public static explicit operator Vector3(Vector2 v2)
        {
            return new Vector3(v2.X, v2.Y, 0); 
        }
    }
}
