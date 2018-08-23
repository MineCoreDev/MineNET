using MineNET.Data;

namespace MineNET.Network.MinecraftPackets
{
    public class PlayerHotbarPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.PLAYER_HOTBAR_PACKET;

        public uint SelectedHotbarSlot { get; set; }
        public byte WindowId { get; set; } = (byte) ContainerIds.INVENTORY;
        public bool SelectHotbarSlot { get; set; } = true;

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt(this.SelectedHotbarSlot);
            this.WriteByte(this.WindowId);
            this.WriteBool(this.SelectHotbarSlot);
        }

        public override void Decode()
        {
            base.Decode();

            this.SelectedHotbarSlot = this.ReadUVarInt();
            this.WindowId = this.ReadByte();
            this.SelectHotbarSlot = this.ReadBool();
        }
    }
}
