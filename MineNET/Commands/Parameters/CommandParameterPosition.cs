namespace MineNET.Commands.Parameters
{
    public class CommandParameterPosition : CommandParameter
    {
        public CommandParameterPosition(string name, bool optional = true, string postfix = null)
            : base(name, CommandParameter.ARG_TYPE_POSITION, optional, null, postfix)
        {

        }
    }
}
