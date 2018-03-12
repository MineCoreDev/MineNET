using MineNET.Data;

namespace MineNET.Network.Packets
{
    public class ResourcePackStackPacket : DataPacket
    {
        public const int ID = ProtocolInfo.RESOURCE_PACK_STACK_PACKET;

        public override byte PacketID
        {
            get
            {
                return ResourcePackStackPacket.ID;
            }
        }

        public bool MustAccept { get; set; } = false;

        public ResourcePack[] BehaviourPackEntries { get; set; } = new ResourcePack[0];

        public ResourcePack[] ResourcePackEntries { get; set; } = new ResourcePack[0];

        public override void Encode()
        {
            base.Encode();

            this.WriteBool(this.MustAccept);
            this.WriteShort((short) this.BehaviourPackEntries.Length);
            for (int i = 0; i < this.BehaviourPackEntries.Length; ++i)
            {
                ResourcePack entry = this.BehaviourPackEntries[i];
                this.WriteString(entry.GetPackId());
                this.WriteString(entry.GetPackVersion());
            }
            this.WriteShort((short) this.ResourcePackEntries.Length);
            for (int i = 0; i < this.ResourcePackEntries.Length; ++i)
            {
                ResourcePack entry = this.ResourcePackEntries[i];
                this.WriteString(entry.GetPackId());
                this.WriteString(entry.GetPackVersion());
            }
        }
    }
}