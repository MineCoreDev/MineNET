namespace MineNET.Commands.Parameters
{
    public class CommandParameterTarget : CommandParameter
    {
        public CommandParameterTarget(string name, bool optional = true, string postfix = null)
            : base(name, CommandParameter.ARG_TYPE_TARGET, optional, null, postfix)
        {

        }
    }
}
