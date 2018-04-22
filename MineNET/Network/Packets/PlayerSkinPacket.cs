using MineNET.Entities.Data;
using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class PlayerSkinPacket : DataPacket
    {
        public const int ID = ProtocolInfo.PLAYER_SKIN_PACKET;

        public override byte PacketID
        {
            get
            {
                return PlayerSkinPacket.ID;
            }
        }

        public UUID Uuid { get; set; }
        public Skin Skin { get; set; }
        public string NewSkinName { get; set; } = "";
        public string OldSkinName { get; set; } = "";

        public override void Encode()
        {
            base.Encode();

            this.WriteUUID(this.Uuid);
            this.WriteString(this.Skin.SkinId);
            this.WriteString(this.NewSkinName);
            this.WriteString(this.OldSkinName);
            this.WriteByteData(this.Skin.SkinData);
            this.WriteByteData(this.Skin.CapeData);
            this.WriteString(this.Skin.GeometryName);
            this.WriteString(this.Skin.GeometryData);
        }

        public override void Decode()
        {
            base.Decode();

            this.Uuid = this.ReadUUID();
            string skinId = this.ReadString();
            this.NewSkinName = this.ReadString();
            this.OldSkinName = this.ReadString();
            this.Skin = new Skin(skinId, this.ReadByteData(), this.ReadByteData(), this.ReadString(), this.ReadString());
        }
    }
}
