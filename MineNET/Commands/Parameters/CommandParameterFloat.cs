﻿namespace MineNET.Commands.Parameters
{
    public class CommandParameterFloat : CommandParameter
    {
        public CommandParameterFloat(string name, bool optional = true, string postfix = null)
            : base(name, CommandParameter.ARG_TYPE_FLOAT, optional, null, postfix)
        {

        }
    }
}
