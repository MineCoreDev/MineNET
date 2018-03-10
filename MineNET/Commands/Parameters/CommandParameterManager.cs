using System.Collections.Generic;

namespace MineNET.Commands.Parameters
{
    public class CommandParameterManager
    {
        public List<CommandParameter> CommandParameters { get; } = new List<CommandParameter>();

        public CommandParameterManager AddParameter(CommandParameter parameter)
        {
            this.CommandParameters.Add(parameter);
            return this;
        }
    }
}
