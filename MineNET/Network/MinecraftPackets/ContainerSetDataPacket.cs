namespace MineNET.Network.MinecraftPackets
{
    public class ContainerSetDataPacket : MinecraftPacket
    {
        public const int PROPERTY_FURNACE_TICK_COUNT = 0;
        public const int PROPERTY_FURNACE_LIT_TIME = 1;
        public const int PROPERTY_FURNACE_LIT_DURATION = 2;
        //TODO: check property 3
        public const int PROPERTY_FURNACE_FUEL_AUX = 4;

        public const int PROPERTY_BREWING_STAND_BREW_TIME = 0;
        public const int PROPERTY_BREWING_STAND_FUEL_AMOUNT = 1;
        public const int PROPERTY_BREWING_STAND_FUEL_TOTAL = 2;

        public override byte PacketID { get; } = MinecraftProtocol.CONTAINER_SET_DATA_PACKET;

        public byte WindowId { get; set; }
        public int Property { get; set; }
        public int Value { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.WindowId);
            this.WriteVarInt(this.Property);
            this.WriteVarInt(this.Value);
        }
    }
}
