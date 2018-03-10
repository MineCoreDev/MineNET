using MineNET.Data;

namespace MineNET.Commands.Defaults
{
    public class StopCommand : Command
    {
        public StopCommand()
        {
            /* test
              this.AddParameters(new CommandParameterManager()
                .AddParameter(new CommandParameter("test", CommandParameter.ARG_TYPE_INT)));*/
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

        public override bool Execute(CommandSender sender, params string[] args)
        {
            Server.Instance.Stop();
            return true;
        }
    }
}
