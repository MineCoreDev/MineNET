using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class InteractPacket : MinecraftPacket
    {
        public const int ACTION_LEAVE_VEHICLE = 3;
        public const int ACTION_MOUSEOVER = 4;

        public const int ACTION_OPEN_INVENTORY = 6;

        public override byte PacketID { get; } = MinecraftProtocol.INTERACT_PACKET;

        public byte Action { get; set; }
        public long EntityRuntimeId { get; set; }
        public Vector3 Position { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.Action);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);

            if (this.Action == InteractPacket.ACTION_MOUSEOVER)
            {
                this.WriteVector3(this.Position);
            }
        }

        public override void Decode()
        {
            base.Decode();

            this.Action = this.ReadByte();
            this.EntityRuntimeId = this.ReadEntityRuntimeId();

            if (this.Action == InteractPacket.ACTION_MOUSEOVER)
            {
                this.Position = this.ReadVector3();
            }
        }
    }
}
