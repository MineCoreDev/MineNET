using MineNET.Worlds.Rule;

namespace MineNET.Network.MinecraftPackets
{
    public class GameRulesChangedPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.GAME_RULES_CHANGED_PACKET;

        public GameRules GameRules { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteGameRules(this.GameRules);
        }
    }
}
