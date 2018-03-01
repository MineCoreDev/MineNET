using MineNET.Utils;

namespace MineNET.Network.Packets
{
    public class AvailableCommandsPacket : DataPacket
    {
        public const int ID = ProtocolInfo.AVAILABLE_COMMANDS_PACKET;

        public override byte PacketID
        {
            get
            {
                return AvailableCommandsPacket.ID;
            }
        }

        public const int ARG_TYPE_INT = 0x01;
        public const int ARG_TYPE_FLOAT = 0x02;
        public const int ARG_TYPE_VALUE = 0x03;
        public const int ARG_TYPE_TARGET = 0x04;

        public const int ARG_TYPE_STRING = 0x0d;
        public const int ARG_TYPE_POSITION = 0x0e;

        public const int ARG_TYPE_RAWTEXT = 0x11;
        public const int ARG_TYPE_TEXT = 0x13;

        public const int ARG_TYPE_JSON = 0x16;
        public const int ARG_TYPE_COMMAND = 0x1d;

        public const int ARG_FLAG_VALID = 0x100000;
        public const int ARG_FLAG_ENUM = 0x200000;

        public const int ARG_FLAG_POSTFIX = 0x1000000;

        //TODO : Dictionary<string, CommandData> commands;

        public int AliasCommands { get; set; } = 0;

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt(0);
            this.WriteUVarInt(0);
            this.WriteUVarInt(0);
            this.WriteUVarInt(0);
            this.WriteBytes(new BinaryStream().GetBuffer());//TODO
        }
    }
}
