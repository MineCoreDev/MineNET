using System.Collections.Generic;

namespace MineNET.Commands
{
    public class CommandData
    {
        public CommandSender Sender { get; }

        public string Text { get; }
        public string Command { get; set; }
        public string[] Args { get; set; }

        public CommandData(CommandSender sender, string text)
        {
            this.Sender = sender;
            this.Text = text;
        }

        public void SplitText()
        {
            string[] args = this.Text.Split(' ');

            this.Command = args[0];
            if (args.Length != 1)
            {
                List<string> tmp = new List<string>(args);
                tmp.RemoveAt(0);
                this.Args = tmp.ToArray();
            }
            else
            {
                this.Args = new string[0];
            }
        }
    }
}
