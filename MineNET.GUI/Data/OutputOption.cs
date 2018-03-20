namespace MineNET.GUI.Data
{
    public class OutputOption
    {
        public bool Log { get; set; } = false;
        public bool Info { get; set; } = true;
        public bool Notice { get; set; } = true;
        public bool Warning { get; set; } = true;
        public bool Error { get; set; } = true;
        public bool Fatal { get; set; } = true;
    }
}
