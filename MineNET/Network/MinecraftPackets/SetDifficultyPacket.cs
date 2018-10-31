using MineNET.Worlds;

namespace MineNET.Network.MinecraftPackets
{
    public class SetDifficultyPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_DIFFICULTY_PACKET;

        public Difficulty Difficulty { get; set; }

        protected override void EncodePayload()
        {
            this.WriteUVarInt((uint) this.Difficulty);
        }

        protected override void DecodePayload()
        {
            this.Difficulty = (Difficulty) this.ReadUVarInt();
        }
    }
}
