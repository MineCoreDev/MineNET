using System;
using System.Linq;

namespace MineNET.Values
{
    public struct UUID
    {
        private ulong _a;
        private ulong _b;

        public UUID(byte[] bytes)
        {
            this._a = BitConverter.ToUInt64(bytes.Skip(0).Take(8).Reverse().ToArray(), 0);
            this._b = BitConverter.ToUInt64(bytes.Skip(8).Take(8).Reverse().ToArray(), 0);
        }

        public UUID(string uuidString)
        {
            uuidString = uuidString.Replace("-", "");
            byte[] bytes = StringToByteArray(uuidString);
            this._a = BitConverter.ToUInt64(bytes.Skip(0).Take(8).ToArray(), 0);
            this._b = BitConverter.ToUInt64(bytes.Skip(8).Take(8).ToArray(), 0);
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[0];
            return bytes.Concat(BitConverter.GetBytes(this._a).Reverse())
                .Concat(BitConverter.GetBytes(this._b).Reverse())
                .ToArray();
        }

        public bool Equals(UUID other)
        {
            return this._a == other._a && this._b == other._b;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UUID) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this._a.GetHashCode() * 397) ^ this._b.GetHashCode();
            }
        }

        public override string ToString()
        {
            byte[] bytes = new byte[0];
            bytes = bytes.Concat(BitConverter.GetBytes(this._a))
                .Concat(BitConverter.GetBytes(this._b))
                .ToArray();

            string hex = string.Join("", bytes.Select(b => b.ToString("x2")));

            return hex.Substring(0, 8) + "-" + hex.Substring(8, 4) + "-" + hex.Substring(12, 4) + "-" + hex.Substring(16, 4) + "-" + hex.Substring(20, 12);
            //xxxxxxxx-xxxx-Mxxx-Nxxx-xxxxxxxxxxxx 8-4-4-12
        }
    }
}
