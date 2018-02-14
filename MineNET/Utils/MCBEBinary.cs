using MineNET.Values;

namespace MineNET.Utils
{
    public class MCBEBinary : BinaryStream
    {
        public Vector2 ReadVector2()
        {
            return new Vector2(this.ReadFloat(), this.ReadFloat());
        }

        public void WriteVector2(Vector2 value)
        {
            this.WriteFloat(value.X);
            this.WriteFloat(value.Y);
        }

        public Vector3 ReadVector3()
        {
            return new Vector3(this.ReadFloat(), this.ReadFloat(), this.ReadFloat());
        }

        public void WriteVector3(Vector3 value)
        {
            this.WriteFloat(value.X);
            this.WriteFloat(value.Y);
            this.WriteFloat(value.Z);
        }

        //TODO : ReadBlockPosition

        public void WriteBlockPosition(int x, int y, int z)
        {
            this.WriteVarInt(x);
            this.WriteUVarInt((uint) y);
            this.WriteVarInt(z);
        }

        public long ReadEntityUniqueId()
        {
            return this.ReadVarLong();
        }

        public void WriteEntityUniqueId(long eid)
        {
            this.WriteVarLong(eid);
        }

        public long ReadEntityRuntimeId()
        {
            return (long) this.ReadUVarLong();
        }

        public void WriteEntityRuntimeId(long eid)
        {
            this.WriteUVarLong((ulong) eid);
        }
    }
}
