using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class CommandBlockUpdatePacket : MinecraftPacket
    {
        public override byte PacketID { get; }

        public bool IsBlock { get; set; }
        public BlockCoordinate3D Position { get; set; }
        public uint CommandBlockMode { get; set; }
        public bool IsRedstoneMode { get; set; }
        public bool IsConditional { get; set; }
        public long MinecartEid { get; set; }
        public string Command { get; set; }
        public string LastOutput { get; set; }
        public string Name { get; set; }
        public bool ShouldTrackOutput { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteBool(this.IsBlock);
            if (this.IsBlock)
            {
                this.WriteBlockVector3(this.Position);
                this.WriteUVarInt(this.CommandBlockMode);
                this.WriteBool(this.IsRedstoneMode);
                this.WriteBool(this.IsConditional);
            }
            else
            {
                this.WriteEntityRuntimeId(this.MinecartEid);
            }
            this.WriteString(this.Command);
            this.WriteString(this.LastOutput);
            this.WriteString(this.Name);
            this.WriteBool(this.ShouldTrackOutput);
        }

        public override void Decode()
        {
            base.Decode();

            this.IsBlock = this.ReadBool();
            if (this.IsBlock)
            {
                this.Position = this.ReadBlockVector3();
                this.CommandBlockMode = this.ReadUVarInt();
                this.IsRedstoneMode = this.ReadBool();
                this.IsConditional = this.ReadBool();
            }
            else
            {
                this.MinecartEid = this.ReadEntityRuntimeId();
            }
            this.Command = this.ReadString();
            this.LastOutput = this.ReadString();
            this.Name = this.ReadString();
            this.ShouldTrackOutput = this.ReadBool();
        }
    }
}
