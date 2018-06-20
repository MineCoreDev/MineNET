using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class ChangeDimensionPacket : DataPacket
    {
        public override byte PacketID
        {
            get
            {
                return ProtocolInfo.CHANGE_DIMENSION_PACKET;
            }
        }

        public int Dimension { get; set; }
        public Vector3 Position { get; set; }
        public bool Respawn { get; set; } = false;

        public override void Encode()
        {
            base.Encode();

            this.WriteVarInt(this.Dimension);
            this.WriteVector3(this.Position);
            this.WriteBool(this.Respawn);
        }

        public override void Decode()
        {
            base.Decode();

            this.Dimension = this.ReadVarInt();
            this.Position = this.ReadVector3();
            this.Respawn = this.ReadBool();
        }
    }
}
