﻿using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;

namespace MineNET.Commands.Defaults
{
    public class StopCommand : Command
    {
        public override string Name
        {
            get
            {
                return "stop";
            }
        }

        public override string Description
        {
            get
            {
                return this.LangDescription();
            }
        }

        public override PlayerPermissions CommandPermission
        {
            get
            {
                //return PlayerPermissions.OPERATOR;
                return PlayerPermissions.VISITOR;
            }
        }

        public override CommandOverload[] CommandOverloads
        {
            get
            {
                return new CommandOverload[]
                {
                    new CommandOverload(),
                    new CommandOverload(
                        new CommandParameterString("stopReason", true)
                    )
                };
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            Server.Instance.Stop();
            return true;
        }
    }
}
