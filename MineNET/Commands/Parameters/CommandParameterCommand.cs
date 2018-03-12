namespace MineNET.Commands.Parameters
{
    public class CommandParameterCommand : CommandParameter
    {
        public CommandParameterCommand(string name, bool optional = true, string postfix = null)
            : base(name, CommandParameter.ARG_TYPE_COMMAND, optional, null, postfix)
        {

        }
    }
}
