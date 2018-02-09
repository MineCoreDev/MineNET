namespace MineNET.Commands.Defaults
{
    public class StopCommand : Command
    {
        public override string Alias
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

        public override string Name
        {
            get
            {
                return "default_stopCommand";
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            MineNETServer.Instance.Stop();
            return true;
        }
    }
}
