using System.Collections.Generic;
using System.Linq;
using MineNET.Commands;
using MineNET.Commands.Data;
using MineNET.Commands.Enums;
using MineNET.Commands.Parameters;
using MineNET.Utils;

namespace MineNET.Network.MinecraftPackets
{
    public class AvailableCommandsPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.AVAILABLE_COMMANDS_PACKET;

        public int CommandCount { get; private set; }
        public Dictionary<string, Command> Commands { get; set; } = new Dictionary<string, Command>();
        public List<CommandEnum> Enums { get; set; } = new List<CommandEnum>();
        public Dictionary<string, List<string>> SoftEnums { get; set; } = new Dictionary<string, List<string>>();
        public List<string> PostFixes { get; set; } = new List<string>();

        protected override void EncodePayload()
        {
            List<string> enumValues = new List<string>();
            this.CommandCount = 0;
            this.InitEnums(enumValues);

            byte[] result = null;
            using (BinaryStream stream = new BinaryStream())
            {
                foreach (Command command in this.Commands.Values)
                {
                    if (command.Name == "help")
                    {
                        continue;
                    }

                    if (command.IsAliasesCommand)
                    {
                        continue;
                    }

                    this.WriteCommand(command, enumValues, stream);
                    this.CommandCount++;
                }
                result = stream.ToArray();
            }

            this.WriteEnumValues(enumValues);
            this.WritePostfixes(this.PostFixes);
            this.WriteEnums(this.Enums, enumValues);

            this.WriteUVarInt((uint) this.CommandCount);
            this.WriteBytes(result);

            this.WriteSoftEnums(this.SoftEnums);
        }

        protected override void DecodePayload()
        {

        }

        private void WriteCommand(Command command, List<string> enumValues, BinaryStream stream)
        {
            stream.WriteString(command.Name);
            if (command.Description.Length > 0)
            {
                string desc = command.Description;
                if (command.Description[0] == '%')
                {
                    desc = desc.Remove(0, 1);
                }

                stream.WriteString(desc);
            }
            else
            {
                stream.WriteString(command.Description);
            }

            stream.WriteByte((byte) command.Flag);
            stream.WriteByte((byte) command.PermissionLevel);

            int enumIndex = -1;
            if (command.Aliases != null && command.Aliases.Length > 0)
            {
                string name = command.Name;
                List<string> aliases = new List<string>();
                for (int i = 0; i < command.Aliases.Length; ++i)
                {
                    string alias = command.Aliases[i];
                    if (enumValues.Contains(alias))
                    {
                        aliases.Add(alias);
                    }
                    else
                    {
                        enumValues.Add(alias);
                        aliases.Add(alias);
                    }
                }

                CommandEnum c = new CommandEnum($"{name}CommandAliases", aliases.ToArray());
                if (this.Enums.Contains(c, new CommandEnumComparer()))
                {
                    enumIndex = this.Enums.IndexOf(c);
                }
                else
                {
                    this.Enums.Add(c);
                    enumIndex = this.Enums.IndexOf(c);
                }
            }
            stream.WriteLInt((uint) enumIndex);
            this.WriteCommandOverloads(command.CommandOverloads, enumValues, stream);
        }

        private void WriteCommandOverloads(CommandOverload[] overloads, List<string> enumValues, BinaryStream stream)
        {
            stream.WriteUVarInt((uint) overloads.Length);
            for (int i = 0; i < overloads.Length; ++i)
            {
                CommandOverload overload = overloads[i];
                List<CommandParameter> parameters = overload.Parameters;
                this.WriteCommandParameters(parameters, enumValues, stream);
            }
        }

        private void WriteCommandParameters(List<CommandParameter> parameters, List<string> enumValues, BinaryStream stream)
        {
            stream.WriteUVarInt((uint) parameters.Count);
            for (int j = 0; j < parameters.Count; ++j)
            {
                CommandParameter parameter = parameters[j];
                this.WriteCommandParameter(parameter, enumValues, stream);
            }
        }

        private void WriteCommandParameter(CommandParameter parameter, List<string> enumValues, BinaryStream stream)
        {
            stream.WriteString(parameter.Name);
            int type = parameter.Type;
            if (parameter.CommandEnum != null && parameter.CommandEnum.Values.Length > 0)
            {
                CommandEnum commandEnum = parameter.CommandEnum;
                List<string> realValue = new List<string>();
                for (int k = 0; k < commandEnum.Values.Length; ++k)
                {
                    string value = commandEnum.Values[k];
                    if (enumValues.Contains(value))
                    {
                        realValue.Add(value);
                    }
                    else
                    {
                        enumValues.Add(value);
                        realValue.Add(value);
                    }
                }

                CommandEnum c = new CommandEnum(commandEnum.Name, realValue.ToArray());
                int enumIndex = -1;
                if (this.Enums.Contains(c, new CommandEnumComparer()))
                {
                    enumIndex = this.Enums.IndexOf(c) & 0xffff;
                }
                else
                {
                    this.Enums.Add(c);
                    enumIndex = this.Enums.IndexOf(c) & 0xffff;
                }
                type = CommandParameter.ARG_FLAG_ENUM | CommandParameter.ARG_FLAG_VALID | enumIndex;
            }
            else if (!string.IsNullOrEmpty(parameter.Postfix))
            {
                int key = -1;
                string postFix = parameter.Postfix;
                if (this.PostFixes.Contains(postFix))
                {
                    key = this.PostFixes.IndexOf(postFix) & 0xffff;
                }
                else
                {
                    this.PostFixes.Add(postFix);
                    key = this.PostFixes.IndexOf(postFix) & 0xffff;
                }
                type = CommandParameter.ARG_FLAG_POSTFIX | CommandParameter.ARG_FLAG_VALID | key;
            }
            else
            {
                type |= CommandParameter.ARG_FLAG_VALID;
            }
            stream.WriteLInt((uint) type);
            stream.WriteBool(parameter.Optional);
        }

        private void InitEnums(List<string> enumValues)
        {
            this.Enums.Add(new CommandEnumGameMode());
            //this.Enums.Add(new CommandEnum("", "name", "type"));
            //this.Enums.Add(new CommandEnumRValueParam());

            for (int i = 0; i < this.Enums.Count; ++i)
            {
                CommandEnum enumData = this.Enums[i];
                for (int j = 0; j < enumData.Values.Length; ++j)
                {
                    string val = enumData.Values[j];
                    if (!enumValues.Contains(val))
                    {
                        enumValues.Add(val);
                    }
                }
            }
        }
    }

    internal class CommandEnumComparer : IEqualityComparer<CommandEnum>
    {
        public bool Equals(CommandEnum x, CommandEnum y)
        {
            return x?.Name == y?.Name;
        }

        public int GetHashCode(CommandEnum obj)
        {
            return obj.Name.GetHashCode() ^ obj.Values.GetHashCode() << 2;
        }
    }
}
