using MineNET.Commands.Data;
using MineNET.Commands.Parameters;
using MineNET.Data;

namespace MineNET.Commands.Defaults
{
    public class StopCommand : Command
    {
        public StopCommand()
        {
            this.RemoveAllOverloads();
            this.AddOverloads(new CommandOverload());
            this.AddOverloads(new CommandOverload(new CommandParameter[]
            {
                new CommandParameterString("reason", false)
            }));
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
                return "Stop Command";//TODO: 日本語が入ると正常に動かない this.LangDescription();
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
