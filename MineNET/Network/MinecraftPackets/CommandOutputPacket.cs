using MineNET.Data;

namespace MineNET.Network.MinecraftPackets
{
    public class CommandOutputPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.COMMAND_OUTPUT_PACKET;

        public CommandOriginData OriginData { get; set; }
        public byte OutputType { get; set; }
        public uint SuccessCount { get; set; }
        public CommandOutputMessage[] Messages { get; set; }
        public string UnknownString { get; set; }

        protected override void EncodePayload()
        {
            this.WriteCommandOriginData(this.OriginData);
            this.WriteByte(this.OutputType);
            this.WriteUVarInt(this.SuccessCount);

            this.WriteUVarInt((uint) this.Messages.Length);
            for (int i = 0; i < this.Messages.Length; ++i)
            {
                this.WriteCommandOutputMessage(this.Messages[i]);
            }

            if (this.OutputType == 4)
            {
                this.WriteString(this.UnknownString);
            }
        }

        protected override void DecodePayload()
        {
            this.OriginData = this.ReadCommandOriginData();
            this.OutputType = this.ReadByte();
            this.SuccessCount = this.ReadUVarInt();

            this.Messages = new CommandOutputMessage[this.ReadUVarInt()];
            for (int i = 0; i < this.Messages.Length; ++i)
            {
                this.Messages[i] = this.ReadCommandOutputMessage();
            }

            if (this.OutputType == 4)
            {
                this.UnknownString = this.ReadString();
            }
        }
    }
}
