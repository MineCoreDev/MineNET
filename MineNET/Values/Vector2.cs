using System;
using MineNET.Utils;

namespace MineNET.Values
{
    public struct Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return this.X;
                }
                if (index != 1)
                {
                    throw new IndexOutOfRangeException();
                }
                return this.Y;
            }

            set
            {
                if (index != 0)
                {
                    if (index != 1)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    this.Y = value;
                }
                else
                {
                    this.X = value;
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

        public override string ToString()
        {
            return $"X: {this.X} Y: {this.Y}";
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            t = MineNETMath.ClampZeroOne(t);
            return new Vector2(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);
        }

        public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);
        }

        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            Vector2 a = target - current;
            float magnitude = a.Magnitude;
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }
            return current + a / magnitude * maxDistanceDelta;
        }

        public static Vector2 Scale(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        public void Scale(Vector2 scale)
        {
            this.X *= scale.X;
            this.Y *= scale.Y;
        }

        public void Normalize()
        {
            float magnitude = this.Magnitude;
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
                Vector2 result = new Vector2(this.X, this.Y);
                result.Normalize();
                return result;
            }
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() << 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector2))
            {
                return false;
            }
            Vector2 vector = (Vector2) other;
            return this.X.Equals(vector.X) && this.Y.Equals(vector.Y);
        }

        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
        {
            return -2f * Vector2.Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        public static float Dot(Vector2 lhs, Vector2 rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y;
        }

        public float Magnitude
        {
            get
            {
                return (float) Math.Sqrt(this.X * this.X + this.Y * this.Y);
            }
        }

        public float SqrtMagnitude
        {
            get
            {
                return this.X * this.X + this.Y * this.Y;
            }
        }

        public static float Angle(Vector2 from, Vector2 to)
        {
            return (float) Math.Acos(MineNETMath.Clamp(Vector2.Dot(from.Normalized, to.Normalized), -1f, 1f)) * 57.29578f;
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            return (a - b).Magnitude;
        }

        public static float DistanceSquared(Vector2 a, Vector2 b)
        {
            return (a - b).SqrtMagnitude;
        }

        public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
        {
            if (vector.SqrtMagnitude > maxLength * maxLength)
            {
                return vector.Normalized * maxLength;
            }
            return vector;
        }

        public static float SqrMagnitude(Vector2 a)
        {
            return a.X * a.X + a.Y * a.Y;
        }

        public static Vector2 Min(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(Math.Min(lhs.X, rhs.X), Math.Min(lhs.Y, rhs.Y));
        }

        public static Vector2 Max(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(Math.Max(lhs.X, rhs.X), Math.Max(lhs.Y, rhs.Y));
        }

        public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = Math.Max(0.0001f, smoothTime);
            float num = 2f / smoothTime;
            float num2 = num * deltaTime;
            float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            Vector2 vector = current - target;
            Vector2 vector2 = target;
            float maxLength = maxSpeed * smoothTime;
            vector = Vector2.ClampMagnitude(vector, maxLength);
            target = current - vector;
            Vector2 vector3 = (currentVelocity + num * vector) * deltaTime;
            currentVelocity = (currentVelocity - num * vector3) * d;
            Vector2 vector4 = target + (vector + vector3) * d;
            if (Vector2.Dot(vector2 - current, vector4 - vector2) > 0f)
            {
                vector4 = vector2;
                currentVelocity = (vector4 - vector2) / deltaTime;
            }
            return vector4;
        }

        public static Vector2 Zero
        {
            get
            {
                return new Vector2(0f, 0f);
            }
        }

        public static Vector2 One
        {
            get
            {
                return new Vector2(1f, 1f);
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

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector3 operator +(Vector2 a, float b)
        {
            return new Vector3(a.X, a.Y, b);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.X, -a.Y);
        }

        public static Vector2 operator *(Vector2 a, float d)
        {
            return new Vector2(a.X * d, a.Y * d);
        }

        public static Vector2 operator *(float d, Vector2 a)
        {
            return new Vector2(a.X * d, a.Y * d);
        }

        public static Vector2 operator /(Vector2 a, float d)
        {
            return new Vector2(a.X / d, a.Y / d);
        }

        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return Vector2.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return Vector2.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
        }

        public static explicit operator Vector2(Vector3 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static explicit operator Vector2(Vector3i v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static explicit operator Vector2(Position p)
        {
            return new Vector2(p.X, p.Y);
        }

        public static explicit operator Vector2(Location l)
        {
            return new Vector2(l.X, l.Y);
        }
    }
}
