using MineNET.Entities.Data;

namespace MineNET.Network.Packets.Data
{
    public sealed class ClientData
    {
        public string ClientRandomID { get; set; }
        public int CurrentInputMode { get; set; }
        public int DefaultInputMode { get; set; }
        public string DeviceModel { get; set; }
        public int DeviceOS { get; set; }
        public string GameVersion { get; set; }
        public int GUIScale { get; set; }
        public string LanguageCode { get; set; }
        public string ServerAddress { get; set; }
        public Skin Skin { get; set; }
        public int UIProfile { get; set; }
    }
}
