using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET.Utils;

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
                return this.y;
            }

            set
            {
                this.y = value;
            }
        }

        public double this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return this.x;
                }
                if (index != 1)
                {
                    throw new IndexOutOfRangeException();
                }
                return this.y;
            }

            set
            {
                if (index != 0)
                {
                    if (index != 1)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    this.y = value;
                }
                else
                {
                    this.x = value;
                }
            }
        }

        public void Set(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, double t)
        {
            t = MineNETMath.ClampZeroOne(t);
            return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }
        
        public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, double t)
        {
            return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        public static Vector2 MoveTowards(Vector2 current, Vector2 target, double maxDistanceDelta)
        {
            Vector2 a = target - current;
            double magnitude = a.Magnitude;
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }
            return current + a / magnitude * maxDistanceDelta;
        }
        
        public static Vector2 Scale(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }
        
        public void Scale(Vector2 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
        }
        
        public void Normalize()
        {
            double magnitude = this.Magnitude;
            if (magnitude > 1E-05f)
            {
                this /= magnitude;
            }
            else
            {
                this = Vector2.Zero;
            }
        }
        
        public Vector2 Normalized
        {
            get
            {
                Vector2 result = new Vector2(this.x, this.y);
                result.Normalize();
                return result;
            }
        }

        public double Magnitude
        {
            get
            {
                return Math.Sqrt(this.x * this.x + this.y * this.y);
            }
        }

        public static double Angle(Vector2 from, Vector2 to)
        {
            return Math.Acos(MineNETMath.Clamp(Vector2.Dot(from.Normalized, to.Normalized), -1f, 1f)) * 57.29578f;
        }

        public static double Distance(Vector2 a, Vector2 b)
        {
            return (a - b).Magnitude;
        }
        
        public static Vector2 ClampMagnitude(Vector2 vector, double maxLength)
        {
            if (vector.SqrMagnitude() > maxLength * maxLength)
            {
                return vector.Normalized * maxLength;
            }
            return vector;
        }

        public static double SqrMagnitude(Vector2 a)
        {
            return a.x * a.x + a.y * a.y;
        }
        
        public double SqrMagnitude()
        {
            return this.x * this.x + this.y * this.y;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2)) return false;
            Vector2 vector = (Vector2)obj;
            return this.x.Equals(vector.x) && this.y.Equals(vector.y);
        }

        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
        {
            return -2d * Dot(inNormal, inDirection) * inNormal + inDirection;
        }
        
        public static double Dot(Vector2 lhs, Vector2 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        public static Vector2 Min(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y));
        }
        
        public static Vector2 Max(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y));
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        
        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.x, -a.y);
        }
        
        public static Vector2 operator *(Vector2 a, double d)
        {
            return new Vector2(a.x * d, a.y * d);
        }
        
        public static Vector2 operator *(double d, Vector2 a)
        {
            return new Vector2(a.x * d, a.y * d);
        }
        
        public static Vector2 operator /(Vector2 a, double d)
        {
            return new Vector2(a.x / d, a.y / d);
        }
        
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
        }
        
        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
        }
        
        public static implicit operator Vector2(Vector3 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static implicit operator Vector3(Vector2 v2)
        {
            return new Vector3(v2.x, v2.y, 0);
        }

        public static Vector2 Zero
        {
            get
            {
                return new Vector2(0f, 0f);
            }
        }
        
        public static Vector2 Up
        {
            get
            {
                return new Vector2(0f, 1f);
            }
        }
        
        public static Vector2 Down
        {
            get
            {
                return new Vector2(0f, -1f);
            }
        }

        public static Vector2 Left
        {
            get
            {
                return new Vector2(-1f, 0f);
            }
        }
        
        public static Vector2 Right
        {
            get
            {
                return new Vector2(1f, 0f);
            }
        }

        public override string ToString()
        {
            return $"X: {this.x} Y: {this.y}";
        }
    }
}
