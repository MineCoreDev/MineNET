namespace MineNET.Commands.Parameters
{
    public class CommandParameterMessage : CommandParameter
    {
        public CommandParameterMessage(string name, bool optional = false)
            : base(name, CommandParameter.ARG_TYPE_MESSAGE, optional, null, null)
        {

        }
    }
}
