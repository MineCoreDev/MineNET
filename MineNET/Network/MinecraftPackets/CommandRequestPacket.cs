using MineNET.Data;

namespace MineNET.Network.MinecraftPackets
{
    public class CommandRequestPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.COMMAND_REQUEST_PACKET;

        public string Command { get; set; }
        public CommandOriginData OriginData { get; set; }
        public bool IsInternal { get; set; }

        protected override void EncodePayload()
        {

        }

        protected override void DecodePayload()
        {
            this.Command = this.ReadString();
            this.OriginData = this.ReadCommandOriginData();
            this.IsInternal = this.ReadBool();
        }
    }
}
