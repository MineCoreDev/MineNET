namespace MineNET.Network.Packets
{
    public class SetDisplayObjectivePacket : DataPacket
    {
        public const int ID = ProtocolInfo.SET_DISPLAY_OBJECTIVE_PACKET;

        public override byte PacketID
        {
            get
            {
                return SetDisplayObjectivePacket.ID;
            }
        }

        public string DisplaySlot { get; set; }
        public string ObjectiveName { get; set; }
        public string DisplayName { get; set; }
        public string CriteriaName { get; set; }
        public int SortOrder { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.DisplaySlot);
            this.WriteString(this.ObjectiveName);
            this.WriteString(this.DisplayName);
            this.WriteString(this.CriteriaName);
            this.WriteSVarInt(this.SortOrder);
        }
    }
}
