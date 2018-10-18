using MineNET.Commands.Enums;

namespace MineNET.Commands.Parameters
{
    public class CommandParameterValueEnum : CommandParameter
    {
        public CommandParameterValueEnum(string value)
            : base("valueEnum", CommandParameter.ARG_TYPE_VALUE, false, new CommandEnum(value, value), null)
        {

        }
    }
}
