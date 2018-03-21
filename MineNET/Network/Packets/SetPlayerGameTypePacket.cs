using MineNET.Entities.Data;

namespace MineNET.Network.Packets
{
    public class SetPlayerGameTypePacket : DataPacket
    {
        public const int ID = ProtocolInfo.SET_PLAYER_GAME_TYPE_PACKET;

        public override byte PacketID
        {
            get
            {
                return SetPlayerGameTypePacket.ID;
            }
        }

        public GameMode GameMode { get; set; }

        public override void Encode()
        {
            base.Encode();

            int gamemode = this.GameMode.GameModeToInt();
            if (gamemode == 3)
            {
                gamemode = 1;
            }
            this.WriteSVarInt(gamemode);
        }
    }
}
