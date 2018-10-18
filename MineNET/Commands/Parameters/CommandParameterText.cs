namespace MineNET.Commands.Parameters
{
    public class CommandParameterText : CommandParameter
    {
        public CommandParameterText(string name, bool optional = false)
       : base(name, CommandParameter.ARG_TYPE_TEXT, optional, null, null)
        {

        }
    }
}
