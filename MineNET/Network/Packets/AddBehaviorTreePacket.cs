namespace MineNET.Network.Packets
{
    public class AddBehaviorTreePacket : DataPacket
    {
        public const int ID = ProtocolInfo.ADD_BEHAVIOR_TREE_PACKET;

        public override byte PacketID
        {
            get
            {
                return AddBehaviorTreePacket.ID;
            }
        }

        public string BehaviorTreeJson { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.BehaviorTreeJson);
        }

        public override void Decode()
        {
            base.Decode();

            this.BehaviorTreeJson = this.ReadString();
        }
    }
}
