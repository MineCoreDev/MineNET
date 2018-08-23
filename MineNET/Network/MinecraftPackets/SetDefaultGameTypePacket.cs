using MineNET.Worlds;

namespace MineNET.Network.MinecraftPackets
{
    public class SetDefaultGameTypePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_DEFAULT_GAME_TYPE_PACKET;

        public GameMode GameMode { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt((uint) this.GameMode);
        }

        public override void Decode()
        {
            base.Decode();

            this.GameMode = (GameMode) this.ReadUVarInt();
        }
    }
}
