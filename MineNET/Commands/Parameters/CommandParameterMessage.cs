namespace MineNET.Commands.Parameters
{
    public class CommandParameterMessage : CommandParameter
    {
        public CommandParameterMessage(string name, bool optional = false, string postfix = null)
            : base(name, CommandParameter.ARG_TYPE_RAWTEXT, optional, null, postfix)
        {

        }
    }
}
