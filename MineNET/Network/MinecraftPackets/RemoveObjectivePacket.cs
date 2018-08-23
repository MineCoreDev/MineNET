namespace MineNET.Network.MinecraftPackets
{
    public class RemoveObjectivePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.REMOVE_OBJECTIVE_PACKET;

        public string ObjectiveName { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.ObjectiveName);
        }

        public override void Decode()
        {
            base.Decode();

            this.ObjectiveName = this.ReadString();
        }
    }
}
