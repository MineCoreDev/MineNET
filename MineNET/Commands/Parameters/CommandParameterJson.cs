namespace MineNET.Commands.Parameters
{
    public class CommandParameterJson : CommandParameter
    {
        public CommandParameterJson(string name, bool optional = false)
            : base(name, CommandParameter.ARG_TYPE_JSON, optional, null, null)
        {

        }
    }
}
