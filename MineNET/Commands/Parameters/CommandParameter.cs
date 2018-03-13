using MineNET.Commands.Data;

namespace MineNET.Commands.Parameters
{
    public class CommandParameter
    {
        public const int ARG_FLAG_VALID = 0x100000;
        public const int ARG_FLAG_ENUM = 0x200000;
        public const int ARG_FLAG_POSTFIX = 0x1000000;

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

        public string Name { get; set; }
        public int Type { get; set; }
        public bool Optional { get; set; }
        public CommandEnum CommandEnum { get; set; }
        public string Postfix { get; set; }

        public CommandParameter(string name, int type, bool optional = false, CommandEnum commandEnum = null, string postfix = null)
        {
            this.Name = name;
            this.Type = type;
            this.Optional = optional;
            this.CommandEnum = commandEnum;
            this.Postfix = postfix;
        }
    }
}
