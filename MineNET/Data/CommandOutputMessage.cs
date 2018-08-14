namespace MineNET.Data
{
    public class CommandOutputMessage
    {
        public bool IsInternal { get; set; }
        public string MessageId { get; set; }
        public string[] Parameters { get; set; }
    }
}
