namespace MineNET.Events.IOEvents
{
    public class InputActionEventArgs : IOEventArgs
    {
        public string InputText { get; set; }

        public InputActionEventArgs(string inputText)
        {
            this.InputText = inputText;
        }
    }
}
