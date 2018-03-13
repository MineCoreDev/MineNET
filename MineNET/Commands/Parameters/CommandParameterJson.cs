namespace MineNET.Commands.Parameters
{
    public class CommandParameterJson : CommandParameter
    {
        public CommandParameterJson(string name, bool optional = false, string postfix = null)
            : base(name, CommandParameter.ARG_TYPE_JSON, optional, null, postfix)
        {

        }
    }
}
