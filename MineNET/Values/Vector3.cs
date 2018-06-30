using MineNET.Utils;
using MineNET.Worlds;
using System;

namespace MineNET.Values
{
    public struct Vector3 : IVector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.X;
                    case 1:
                        return this.Y;
                    case 2:
                        return this.Z;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }

            set
            {
                switch (index)
                {
                    case 0:
                        this.X = value;
                        break;
                    case 1:
                        this.Y = value;
                        break;
                    case 2:
                        this.Z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        public int FloorX
        {
            get
            {
                return (int) Math.Floor(this.X);
            }
        }

        public int FloorY
        {
            get
            {
                return (int) Math.Floor(this.Y);
            }
        }

        public int FloorZ
        {
            get
            {
                return (int) Math.Floor(this.Z);
            }
        }

        public override string ToString()
        {
            return $"X: {this.X} Y: {this.Y} Z: {this.Z}";
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            t = MineNETMath.ClampZeroOne(t);
            return new Vector3(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t);
        }

        public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
        {
            return new Vector3(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t);
        }

        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            Vector3 a = target - current;
            float magnitude = a.Magnitude;
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }
            return current + a / magnitude * maxDistanceDelta;
        }

        public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = Math.Max(0.0001f, smoothTime);
            float num = 2f / smoothTime;
            float num2 = num * deltaTime;
            float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            Vector3 vector = current - target;
            Vector3 vector2 = target;
            float maxLength = maxSpeed * smoothTime;
            vector = Vector3.ClampMagnitude(vector, maxLength);
            target = current - vector;
            Vector3 vector3 = (currentVelocity + num * vector) * deltaTime;
            currentVelocity = (currentVelocity - num * vector3) * d;
            Vector3 vector4 = target + (vector + vector3) * d;
            if (Vector3.Dot(vector2 - current, vector4 - vector2) > 0f)
            {
                vector4 = vector2;
                currentVelocity = (vector4 - vector2) / deltaTime;
            }
            return vector4;
        }

        public static Vector3 Scale(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public void Scale(Vector3 scale)
        {
            this.X *= scale.X;
            this.Y *= scale.Y;
            this.Z *= scale.Z;
        }

        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.Y * rhs.Z - lhs.Z * rhs.Y, lhs.Z * rhs.X - lhs.X * rhs.Z, lhs.X * rhs.Y - lhs.Y * rhs.X);
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() << 2 ^ this.Z.GetHashCode() >> 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector3))
            {
                return false;
            }
            Vector3 vector = (Vector3) other;
            return this.X.Equals(vector.X) && this.Y.Equals(vector.Y) && this.Z.Equals(vector.Z);
        }

        public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
        {
            return -2f * Vector3.Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        public static Vector3 Normalize(Vector3 value)
        {
            float num = value.Magnitude;
            if (num > 1E-05f)
            {
                return value / num;
            }
            return Vector3.Zero;
        }

        public void Normalize()
        {
            float num = this.Magnitude;
            if (num > 1E-05f)
            {
                this /= num;
            }
            else
            {
                this = Vector3.Zero;
            }
        }

        public Vector3 Normalized
        {
            get
            {
                return Vector3.Normalize(this);
            }
        }

        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
        }

        public static Vector3 Project(Vector3 vector, Vector3 onNormal)
        {
            float num = Vector3.Dot(onNormal, onNormal);
            if (num < float.Epsilon)
            {
                return Vector3.Zero;
            }
            return onNormal * Vector3.Dot(vector, onNormal) / num;
        }

        public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
        {
            return vector - Vector3.Project(vector, planeNormal);
        }

        public static float Angle(Vector3 from, Vector3 to)
        {
            return (float) Math.Acos(MineNETMath.Clamp(Vector3.Dot(from.Normalized, to.Normalized), -1f, 1f)) * 57.29578f;
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            Vector3 vector = new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
            return (float) Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
        }

        public static float DistanceSquared(Vector3 a, Vector3 b)
        {
            return (a - b).SqrtMagnitude;
        }

        public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
        {
            if (vector.SqrtMagnitude > maxLength * maxLength)
            {
                return vector.Normalized * maxLength;
            }
            return vector;
        }

        public float Magnitude
        {
            get
            {
                return (float) Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
            }
        }

        public static float SqrMagnitude(Vector3 a)
        {
            return a.X * a.X + a.Y * a.Y + a.Z * a.Z;
        }

        public float SqrtMagnitude
        {
            get
            {
                return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
            }
        }

        public static Vector3 Min(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(Math.Min(lhs.X, rhs.X), Math.Min(lhs.Y, rhs.Y), Math.Min(lhs.Z, rhs.Z));
        }

        public static Vector3 Max(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(Math.Max(lhs.X, rhs.X), Math.Max(lhs.Y, rhs.Z), Math.Max(lhs.Z, rhs.Z));
        }

        public static Vector3 Zero
        {
            get
            {
                return new Vector3(0f, 0f, 0f);
            }
        }

        public static Vector3 One
        {
            get
            {
                return new Vector3(1f, 1f, 1f);
            }
        }

        public static Vector3 Forward
        {
            get
            {
                return new Vector3(0f, 0f, 1f);
            }
        }

        public static Vector3 Back
        {
            get
            {
                return new Vector3(0f, 0f, -1f);
            }
        }

        public static Vector3 Up
        {
            get
            {
                return new Vector3(0f, 1f, 0f);
            }
        }

        public static Vector3 Down
        {
            get
            {
                return new Vector3(0f, -1f, 0f);
            }
        }

        public static Vector3 Left
        {
            get
            {
                return new Vector3(-1f, 0f, 0f);
            }
        }

        public static Vector3 Right
        {
            get
            {
                return new Vector3(1f, 0f, 0f);
            }
        }

        public Position Position(World world)
        {
            return new Position(this.X, this.Y, this.Z, world);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.X, -a.Y, -a.Z);
        }

        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.X * d, a.Y * d, a.Z * d);
        }

        public static Vector3 operator *(float d, Vector3 a)
        {
            return new Vector3(a.X * d, a.Y * d, a.Z * d);
        }

        public static Vector3 operator /(Vector3 a, float d)
        {
            return new Vector3(a.X / d, a.Y / d, a.Z / d);
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return Vector3.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return Vector3.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
        }

        public static implicit operator Vector3(Vector2 v)
        {
            return new Vector3(v.X, v.Y, 0f);
        }

        public static explicit operator Vector3(Position p)
        {
            return new Vector3(p.X, p.Y, p.Z);
        }

        public static explicit operator Vector3(Location l)
        {
            return new Vector3(l.X, l.Y, l.Z);
        }
    }
}
