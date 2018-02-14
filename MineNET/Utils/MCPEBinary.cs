using MineNET.Values;

namespace MineNET.Utils
{
    public sealed class MCPEBinary
    {
        public static Vector2 ReadVector2(BinaryStream stream)
        {
            return new Vector2(stream.ReadFloat(), stream.ReadFloat());
        }

        public static void WriteVector2(BinaryStream stream, Vector2 value)
        {
            stream.WriteFloat(value.X);
            stream.WriteFloat(value.Y);
        }

        public static Vector3 ReadVector3(BinaryStream stream)
        {
            return new Vector3(stream.ReadFloat(), stream.ReadFloat(), stream.ReadFloat());
        }

        public static void WriteVector3(BinaryStream stream, Vector3 value)
        {
            stream.WriteFloat(value.X);
            stream.WriteFloat(value.Y);
            stream.WriteFloat(value.Z);
        }
    }
}
