using MineNET.ResourcePack;

namespace MineNET.Network.MinecraftPackets
{
    public class ResourcePacksInfoPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.RESOURCE_PACKS_INFO_PACKET;

        public bool MustAccept { get; set; } = false;
        public IResourcePack[] BehaviourPackEntries { get; set; } = new IResourcePack[0];
        public IResourcePack[] ResourcePackEntries { get; set; } = new IResourcePack[0];

        public override void Encode()
        {
            base.Encode();

            this.WriteBool(this.MustAccept);
            this.WriteShort((short) this.BehaviourPackEntries.Length);
            for (int i = 0; i < this.BehaviourPackEntries.Length; ++i)
            {
                IResourcePack entry = this.BehaviourPackEntries[i];
                this.WriteString(entry.GetPackId());
                this.WriteString(entry.GetPackVersion());
                this.WriteLLong((ulong) entry.GetPackSize());
                this.WriteString("");//TODO:
                this.WriteString("");//TODO:
            }
            this.WriteShort((short) this.ResourcePackEntries.Length);
            for (int i = 0; i < this.ResourcePackEntries.Length; ++i)
            {
                IResourcePack entry = this.ResourcePackEntries[i];
                this.WriteString(entry.GetPackId());
                this.WriteString(entry.GetPackVersion());
                this.WriteLLong((ulong) entry.GetPackSize());
                this.WriteString("");//TODO:
                this.WriteString("");//TODO:
            }
        }
    }
}
