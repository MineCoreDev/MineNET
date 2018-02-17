namespace MineNET.Network.Packets
{
    using MineNET.Data;

    public class ResourcePacksInfoPacket : DataPacket
    {

        public const int ID = ProtocolInfo.RESOURCE_PACKS_INFO_PACKET;

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
            this.WriteLShort((ushort) this.behaviourPackEntries.Length);
            for (int i = 0; i < this.behaviourPackEntries.Length; ++i)
            {
                ResourcePack entry = this.behaviourPackEntries[i];
                this.WriteString(entry.GetPackId());
                this.WriteString(entry.GetPackVersion());
                this.WriteLLong((ulong) entry.GetPackSize());
                this.WriteString("");//TODO
                this.WriteString("");//TODO
            }
            this.WriteLShort((ushort) this.resourcePackEntries.Length);
            for (int i = 0; i < this.resourcePackEntries.Length; ++i)
            {
                ResourcePack entry = this.resourcePackEntries[i];
                this.WriteString(entry.GetPackId());
                this.WriteString(entry.GetPackVersion());
                this.WriteLLong((ulong) entry.GetPackSize());
                this.WriteString("");//TODO
                this.WriteString("");//TODO
            }
        }
    }
}
