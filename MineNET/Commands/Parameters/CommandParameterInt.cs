namespace MineNET.Commands.Parameters
{
    public class CommandParameterInt : CommandParameter
    {
        public CommandParameterInt(string name, bool optional = false, string postfix = null)
            : base(name, CommandParameter.ARG_TYPE_INT, optional, null, postfix)
        {

        }
    }
}
