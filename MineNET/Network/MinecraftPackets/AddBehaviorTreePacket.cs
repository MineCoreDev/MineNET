namespace MineNET.Network.MinecraftPackets
{
    public class AddBehaviorTreePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ADD_BEHAVIOR_TREE_PACKET;

        public string BehaviorTreeJson { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.BehaviorTreeJson);
        }

        protected override void DecodePayload()
        {
            this.BehaviorTreeJson = this.ReadString();
        }
    }
}
