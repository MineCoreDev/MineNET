namespace MineNET.Network.MinecraftPackets
{
    public class AddBehaviorTreePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ADD_BEHAVIOR_TREE_PACKET;

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
