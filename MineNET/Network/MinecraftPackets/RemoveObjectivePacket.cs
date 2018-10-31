namespace MineNET.Network.MinecraftPackets
{
    public class RemoveObjectivePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.REMOVE_OBJECTIVE_PACKET;

        public string ObjectiveName { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.ObjectiveName);
        }

        protected override void DecodePayload()
        {
            this.ObjectiveName = this.ReadString();
        }
    }
}
