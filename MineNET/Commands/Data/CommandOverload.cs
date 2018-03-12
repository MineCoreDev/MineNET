using System.Collections.Generic;
using MineNET.Commands.Parameters;

namespace MineNET.Commands.Data
{
    public class CommandOverload
    {
        public List<CommandParameter> Parameters { get; set; } = new List<CommandParameter>();

        public CommandOverload(params CommandParameter[] parameters)
        {
            for (int i = 0; i < parameters.Length; ++i)
            {
                this.Parameters.Add(parameters[i]);
            }
        }

        public CommandOverload AddParameter(params CommandParameter[] parameters)
        {
            for (int i = 0; i < parameters.Length; ++i)
            {
                this.Parameters.Add(parameters[i]);
            }
            return this;
        }
    }
}
