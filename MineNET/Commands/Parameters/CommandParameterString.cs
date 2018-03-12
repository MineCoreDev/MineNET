using MineNET.Commands.Data;

namespace MineNET.Commands.Parameters
{
    public class CommandParameterString : CommandParameter
    {
        public CommandParameterString(string name, bool optional = true, CommandEnum commandEnum = null, string postfix = null)
            : base(name, CommandParameter.ARG_TYPE_STRING, optional, commandEnum, postfix)
        {

        }
    }
}
