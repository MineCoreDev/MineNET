namespace MineNET.Events.IOEvents
{
    public class OutputActionEventArgs : IOEventArgs
    {
        public string OutputText { get; set; }

        public OutputActionEventArgs(string text)
        {
            this.OutputText = text;
        }
    }
}
