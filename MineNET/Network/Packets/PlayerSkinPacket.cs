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
    }
}
