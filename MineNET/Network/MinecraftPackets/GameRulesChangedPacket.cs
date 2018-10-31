using MineNET.Worlds.Rule;

namespace MineNET.Network.MinecraftPackets
{
    public class GameRulesChangedPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.GAME_RULES_CHANGED_PACKET;

        public GameRules GameRules { get; set; }

        protected override void EncodePayload()
        {
            this.WriteGameRules(this.GameRules);
        }

        protected override void DecodePayload()
        {

        }
    }
}
