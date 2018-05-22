using MineNET.Values;

namespace MineNET.Network.Packets.Data
{
    public class ScorePacketEntry
    {
        public UUID UUID { get; set; }

        public string ObjectiveName { get; set; }

        public int Score { get; set; }
    }
}
