using System.Collections.Generic;
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

        public Dictionary<string, Command> Commands { get; set; } = new Dictionary<string, Command>();
        public Dictionary<string, List<string>> SoftEnums { get; set; } = new Dictionary<string, List<string>>();

        public override void Encode()
        {
            base.Encode();

            List<CommandEnumCash> enums = new List<CommandEnumCash>();
            List<string> enumValues = new List<string>();
            List<string> postFixes = new List<string>();

            byte[] result = null;
            using (BinaryStream stream = new BinaryStream())
            {
                foreach (Command command in this.Commands.Values)
                {
                    if (command.Name == "help")
                    {
                        continue;
                    }

                    stream.WriteString(command.Name);
                    if (!string.IsNullOrEmpty(command.Description))
                    {
                        if (command.Description[0] == '%')
                        {
                            stream.WriteString(command.Description.Remove(0, 1));
                        }
                        else
                        {
                            stream.WriteString(command.Description);
                        }
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
                        List<int> aliases = new List<int>();
                        for (int i = 0; i < command.Aliases.Length; ++i)
                        {
                            enumValues.Add(command.Aliases[i]);
                            aliases.Add(enumValues.Count - 1);
                        }
                        enumValues.Add(command.Name);
                        aliases.Add(enumValues.Count - 1);

                        enums.Add(new CommandEnumCash($"{command.Name}CommandAliases", aliases.ToArray()));
                        enumIndex = enums.Count - 1;
                    }
                    stream.WriteLInt((uint) enumIndex);

                    CommandOverload[] overloads = command.CommandOverloads;
                    stream.WriteUVarInt((uint) overloads.Length);
                    for (int i = 0; i < overloads.Length; ++i)
                    {
                        CommandOverload overload = overloads[i];
                        List<CommandParameter> parameters = overload.Parameters;
                        stream.WriteUVarInt((uint) parameters.Count);
                        for (int j = 0; j < parameters.Count; ++j)
                        {
                            CommandParameter parameter = parameters[j];
                            stream.WriteString(parameter.Name);
                            int type = parameter.Type;
                            if (parameter.CommandEnum != null && parameter.CommandEnum.Values.Length > 0)
                            {
                                CommandEnum commandEnum = parameter.CommandEnum;
                                List<int> realValue = new List<int>();
                                for (int k = 0; k < commandEnum.Values.Length; ++k)
                                {
                                    string value = commandEnum.Values[k];
                                    enumValues.Add(value);
                                    realValue.Add(enumValues.Count - 1);
                                }
                                enums.Add(new CommandEnumCash(commandEnum.Name, realValue.ToArray()));
                                enumIndex = enums.Count - 1;
                                type = CommandParameter.ARG_FLAG_ENUM | CommandParameter.ARG_FLAG_VALID | enumIndex;
                            }
                            else if (!string.IsNullOrEmpty(parameter.Postfix))
                            {
                                postFixes.Add(parameter.Postfix);
                                postFixes.Add(parameter.Postfix);//Dummy
                                postFixes.Add(parameter.Postfix);//Dummy

                                int key = postFixes.Count - 1;
                                type = CommandParameter.ARG_FLAG_POSTFIX | key;
                            }
                            else
                            {
                                type |= CommandParameter.ARG_FLAG_VALID;
                            }
                            stream.WriteLInt((uint) type);
                            stream.WriteBool(parameter.Optional);
                        }
                    }
                }
                result = stream.ToArray();
            }

            this.WriteUVarInt((uint) enumValues.Count);
            for (int i = 0; i < enumValues.Count; ++i)
            {
                this.WriteString(enumValues[i]);
            }

            this.WriteUVarInt((uint) postFixes.Count);
            for (int i = 0; i < postFixes.Count; ++i)
            {
                this.WriteString(postFixes[i]);
            }

            this.WriteUVarInt((uint) enums.Count);
            for (int i = 0; i < enums.Count; ++i)
            {
                CommandEnumCash cash = enums[i];
                this.WriteString(cash.Name);
                this.WriteUVarInt((uint) cash.Index.Length);
                for (int j = 0; j < cash.Index.Length; ++j)
                {
                    if (enumValues.Count < 0x100)
                    {
                        this.WriteByte((byte) cash.Index[j]);
                    }
                    else if (enumValues.Count < 0x10000)
                    {
                        this.WriteLShort((ushort) cash.Index[j]);
                    }
                    else
                    {
                        this.WriteLInt((uint) cash.Index[j]);
                    }
                }
            }

            this.WriteUVarInt((uint) this.Commands.Count);
            this.WriteBytes(result);

            this.WriteUVarInt((uint) this.SoftEnums.Count);
            foreach (KeyValuePair<string, List<string>> softEnum in this.SoftEnums)
            {
                int count = softEnum.Value.Count;
                this.WriteString(softEnum.Key);
                this.WriteUVarInt((uint) count);
                for (int i = 0; i < count; ++i)
                {
                    this.WriteString(softEnum.Value[i]);
                }
            }
        }
    }

    internal class CommandEnumCash
    {
        public string Name { get; }
        public int[] Index { get; }

        public CommandEnumCash(string name, params int[] index)
        {
            this.Name = name;
            this.Index = index;
        }
    }
}
