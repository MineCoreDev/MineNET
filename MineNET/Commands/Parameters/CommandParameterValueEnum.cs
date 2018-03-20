using MineNET.Commands.Enums;

namespace MineNET.Commands.Parameters
{
    public class CommandParameterValueEnum : CommandParameter
    {
        public CommandParameterValueEnum(string value, bool optional = false, string postfix = null)
            : base("valueEnum", CommandParameter.ARG_TYPE_VALUE, optional, new CommandEnum(value, value), postfix)
        {

        }
    }
}
