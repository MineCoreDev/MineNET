using System.Linq;

namespace MineNET.Network.RakNetPackets
{
    public abstract class OfflineMessage : RakNetPacket
    {
        public byte[] Magic { get; private set; }
        public void ReadMagic()
        {
            this.Magic = this.ReadBytes(RakNetProtocol.Magic.Length);
        }

        public void WriteMagic()
        {
            this.WriteBytes(RakNetProtocol.Magic);
        }

        public bool IsValid()
        {
            return this.Magic.SequenceEqual(RakNetProtocol.Magic);
        }
    }
}
