﻿namespace MineNET.Network.MinecraftPackets
{
    public class SetDisplayObjectivePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_DISPLAY_OBJECTIVE_PACKET;

        public string DisplaySlot { get; set; }
        public string ObjectiveName { get; set; }
        public string DisplayName { get; set; }
        public string CriteriaName { get; set; }
        public int SortOrder { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.DisplaySlot);
            this.WriteString(this.ObjectiveName);
            this.WriteString(this.DisplayName);
            this.WriteString(this.CriteriaName);
            this.WriteSVarInt(this.SortOrder);
        }

        protected override void DecodePayload()
        {
            this.DisplaySlot = this.ReadString();
            this.ObjectiveName = this.ReadString();
            this.DisplayName = this.ReadString();
            this.CriteriaName = this.ReadString();
            this.SortOrder = this.ReadSVarInt();
        }
    }
}
