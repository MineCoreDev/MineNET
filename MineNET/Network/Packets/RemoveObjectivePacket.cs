namespace MineNET.Network.Packets
{
    public class RemoveObjectivePacket : DataPacket
    {
        public const int ID = ProtocolInfo.REMOVE_OBJECTIVE_PACKET;

        public override byte PacketID
        {
            get
            {
                return RemoveObjectivePacket.ID;
            }
        }

        public string ObjectiveName { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.ObjectiveName);
        }
    }
}
