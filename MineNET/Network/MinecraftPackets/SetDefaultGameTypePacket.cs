using MineNET.Worlds;

namespace MineNET.Network.MinecraftPackets
{
    public class SetDefaultGameTypePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_DEFAULT_GAME_TYPE_PACKET;

        public GameMode GameMode { get; set; }

        protected override void EncodePayload()
        {
            this.WriteUVarInt((uint) this.GameMode);
        }

        protected override void DecodePayload()
        {
            this.GameMode = (GameMode) this.ReadUVarInt();
        }
    }
}
