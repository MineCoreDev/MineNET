using MineNET.Worlds;

namespace MineNET.Network.MinecraftPackets
{
    public class SetDifficultyPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_DIFFICULTY_PACKET;

        public Difficulty Difficulty { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt((uint) this.Difficulty);
        }

        public override void Decode()
        {
            base.Decode();

            this.Difficulty = (Difficulty) this.ReadUVarInt();
        }
    }
}
