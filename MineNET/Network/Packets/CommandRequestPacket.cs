using MineNET.Network.Packets.Data;

namespace MineNET.Network.Packets
{
    public class CommandRequestPacket : DataPacket
    {
        public const int ID = ProtocolInfo.COMMAND_REQUEST_PACKET;

        public override byte PacketID
        {
            get
            {
                return CommandRequestPacket.ID;
            }
        }

        public string Command { get; set; }
        public CommandOriginData OriginData { get; set; }
        public bool IsInternal { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.Command = this.ReadString();
            this.OriginData = this.ReadCommandOriginData();
            this.IsInternal = this.ReadBool();
        }
    }
}
