using MineNET.Worlds.Data;

namespace MineNET.Network.Packets
{
    public class GameRulesChangedPacket : DataPacket
    {
        public const int ID = ProtocolInfo.GAME_RULES_CHANGED_PACKET;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        public GameRules GameRules { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteGameRules(this.GameRules);
        }
    }
}
