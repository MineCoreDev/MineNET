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
                return ID;
            }
        }
        bool mustAccept = false;
        public bool MustAccepet
        {
            get
            {
                return this.mustAccept;
            }

            set
            {
                this.mustAccept = value;
            }
        }

        ResourcePack[] behaviourPackEntries = new ResourcePack[0];
        public ResourcePack[] BehaviourPackEntries
        {
            get
            {
                return this.behaviourPackEntries;
            }

            set
            {
                this.behaviourPackEntries = value;
            }
        }

        ResourcePack[] resourcePackEntries = new ResourcePack[0];
        public ResourcePack[] ResourcePackEntries
        {
            get
            {
                return this.resourcePackEntries;
            }

            set
            {
                this.resourcePackEntries = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            this.WriteBool(this.mustAccept);
            this.WriteUVarInt((uint) this.behaviourPackEntries.Length);
            for (int i = 0; i < this.behaviourPackEntries.Length; i++)
            {
                ResourcePack entry = this.behaviourPackEntries[i];
                this.WriteString(entry.GetPackId());
                this.WriteString(entry.GetPackVersion());
            }
            this.WriteUVarInt((uint) this.resourcePackEntries.Length);
            for (int i = 0; i < this.resourcePackEntries.Length; i++)
            {
                ResourcePack entry = this.resourcePackEntries[i];
                this.WriteString(entry.GetPackId());
                this.WriteString(entry.GetPackVersion());
            }
        }
    }
}
