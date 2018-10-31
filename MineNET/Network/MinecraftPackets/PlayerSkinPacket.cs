using MineNET.Data;
using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class PlayerSkinPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.PLAYER_SKIN_PACKET;

        public UUID Uuid { get; set; }
        public Skin Skin { get; set; }
        public string NewSkinName { get; set; } = "";
        public string OldSkinName { get; set; } = "";
        public bool Premium { get; set; }

        protected override void EncodePayload()
        {
            this.WriteUUID(this.Uuid);
            this.WriteString(this.Skin.SkinId);
            this.WriteString(this.NewSkinName);
            this.WriteString(this.OldSkinName);
            this.WriteByteData(this.Skin.SkinData);
            this.WriteByteData(this.Skin.CapeData);
            this.WriteString(this.Skin.GeometryName);
            this.WriteString(this.Skin.GeometryData);
            this.WriteBool(this.Premium);
        }

        protected override void DecodePayload()
        {
            this.Uuid = this.ReadUUID();
            string skinId = this.ReadString();
            this.NewSkinName = this.ReadString();
            this.OldSkinName = this.ReadString();
            this.Skin = new Skin(skinId, this.ReadByteData(), this.ReadByteData(), this.ReadString(), this.ReadString());
            this.Premium = this.ReadBool();
        }
    }
}
