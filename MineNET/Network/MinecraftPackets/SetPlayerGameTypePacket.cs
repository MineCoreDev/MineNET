using MineNET.Worlds;

namespace MineNET.Network.MinecraftPackets
{
    public class SetPlayerGameTypePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_PLAYER_GAME_TYPE_PACKET;

        public GameMode GameMode { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteVarInt((int) this.GameMode);
        }

        public override void Decode()
        {
            base.Decode();

            this.GameMode = (GameMode) this.ReadVarInt();
        }
    }
}
