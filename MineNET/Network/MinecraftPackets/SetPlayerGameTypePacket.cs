using MineNET.Worlds;

namespace MineNET.Network.MinecraftPackets
{
    public class SetPlayerGameTypePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_PLAYER_GAME_TYPE_PACKET;

        public GameMode GameMode { get; set; }

        protected override void EncodePayload()
        {
            this.WriteSVarInt((int) this.GameMode);
        }

        protected override void DecodePayload()
        {
            this.GameMode = (GameMode) this.ReadSVarInt();
        }
    }
}
