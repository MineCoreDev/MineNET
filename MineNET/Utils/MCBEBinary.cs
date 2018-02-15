using MineNET.Values;

namespace MineNET.Utils
{
    public class MCBEBinary : BinaryStream
    {
        public Vector2 ReadVector2()
        {
            return new Vector2(this.ReadLFloat(), this.ReadLFloat());
        }

        public void WriteVector2(Vector2 value)
        {
            this.WriteLFloat(value.X);
            this.WriteLFloat(value.Y);
        }

        public Vector3 ReadVector3()
        {
            return new Vector3(this.ReadLFloat(), this.ReadLFloat(), this.ReadLFloat());
        }

        public void WriteVector3(Vector3 value)
        {
            this.WriteLFloat(value.X);
            this.WriteLFloat(value.Y);
            this.WriteLFloat(value.Z);
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
