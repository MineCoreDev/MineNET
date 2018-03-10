using System.Collections.Generic;
using MineNET.Commands;
using MineNET.Commands.Parameters;

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

        public const int ARG_FLAG_VALID = 0x100000;

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

        public const int ARG_FLAG_ENUM = 0x200000;

        public const int ARG_FLAG_POSTFIX = 0x1000000;

        public Dictionary<string, Command> commands = new Dictionary<string, Command>();
        public int AliasCommands { get; set; } = 0;

        public override void Encode()
        {
            base.Encode();

            List<string> enumValues = new List<string>();
            List<string> postFixes = new List<string>();
            foreach (Command command in this.commands.Values)
            {
                if (command.Aliases != null)
                {

                }
            }

            this.WriteUVarInt(0);
            this.WriteUVarInt(0);
            this.WriteUVarInt(0);
            this.WriteUVarInt((uint) this.commands.Count);
            foreach (Command command in this.commands.Values)
            {
                this.WriteString(command.Name);
                this.WriteString(command.Description);
                this.WriteByte((byte) command.Flag);
                this.WriteByte((byte) command.Permission);
                if (command.Aliases != null)
                {
                    //TODO
                }
                else
                {
                    this.WriteLInt(0);
                }
                List<CommandParameterManager> managers = command.CommandParameterManagers;
                this.WriteUVarInt((uint) managers.Count);
                for (int i = 0; i < managers.Count; ++i)
                {
                    CommandParameterManager manager = managers[i];
                    List<CommandParameter> parameters = manager.CommandParameters;
                    this.WriteUVarInt((uint) parameters.Count);
                    for (int j = 0; j < parameters.Count; ++j)
                    {
                        CommandParameter parameter = parameters[j];
                        this.WriteString(parameter.Name);
                        int type = 0;
                        if (parameter.CommandEnum != null)
                        {
                            //type = AvailableCommandsPacket.ARG_FLAG_ENUM | AvailableCommandsPacket.ARG_FLAG_VALID;
                        }
                        else if (parameter.Postfix != null)
                        {

                        }
                        else
                        {
                            type = parameter.Type;
                        }
                        this.WriteLInt((uint) type);
                        this.WriteBool(parameter.Optional);
                    }
                }
            }
        }
    }
}
