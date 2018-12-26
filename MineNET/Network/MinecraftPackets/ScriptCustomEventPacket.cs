namespace MineNET.Network.MinecraftPackets
{
    public class ScriptCustomEventPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SCRIPT_CUSTOM_EVENT_PACKET;

        public string EventName { get; set; }
        public string EventData { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.EventName);
            this.WriteString(this.EventData);
        }

        protected override void DecodePayload()
        {
            this.EventName = this.ReadString();
            this.EventData = this.ReadString();
        }
    }
}
