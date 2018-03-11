using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;

namespace MineNET.Commands.Defaults
{
    public class StopCommand : Command
    {
        public StopCommand()
        {
            this.AddOverloads(
                new CommandOverload("")
                    .AddParameter(new CommandParameter("test", CommandParameter.ARG_TYPE_STRING, true))
                    .AddParameter(new CommandParameter("test2", CommandParameter.ARG_TYPE_INT, true)),
                new CommandOverload("aaa")
                    .AddParameter(new CommandParameter("valueEnum", CommandParameter.ARG_TYPE_VALUE, false, new CommandEnum("test", "a", "b")))
                );
        }

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
                //return this.LangDescription();
                return "stop";
            }
        }

        public override PlayerPermissions Permission
        {
            get
            {
                //return PlayerPermissions.OPERATOR;
                return PlayerPermissions.VISITOR;
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new string[] { "sp", "st" };
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            Server.Instance.Stop();
            return true;
        }
    }
}
