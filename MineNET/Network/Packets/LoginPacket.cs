using System.Linq;
using System.Text;
using MineNET.Data;
using MineNET.Utils;
using Newtonsoft.Json.Linq;

namespace MineNET.Network.Packets
{
    public class LoginPacket : DataPacket
    {
        public const int ID = ProtocolInfo.LOGIN_PACKET;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        int protocol;
        public int Protocol
        {
            get
            {
                return protocol;
            }

            set
            {
                protocol = value;
            }
        }

        LoginData loginData;
        public LoginData LoginData
        {
            get
            {
                return loginData;
            }

            set
            {
                loginData = value;
            }
        }

        ClientData clientData;
        public ClientData CllientData
        {
            get
            {
                return clientData;
            }

            set
            {
                clientData = value;
            }
        }

        public override void Decode()
        {
            base.Decode();

            protocol = (int)ReadLInt();

            loginData = new LoginData();
            clientData = new ClientData();

            int len = ReadVarInt();
            BinaryStream stream = new BinaryStream(ReadBytes(len));

            int chainLen = stream.ReadInt();
            string chain = Encoding.UTF8.GetString(stream.ReadBytes(chainLen));
            JObject chainObj = JObject.Parse(chain);
            chain = chainObj.ToString();

            JToken chainToken = chainObj["chain"];
            for (int i = 0; i < chainToken.Count(); ++i)
            {
                JObject jwt = JObject.Parse(JWT.Decode(chainToken[i].ToString()));
                JToken extraData = null;
                if (jwt.TryGetValue("extraData", out extraData))
                {
                    loginData.XUID = extraData["XUID"].ToString();
                    loginData.DisplayName = extraData["displayName"].ToString();
                    loginData.ClientUUID = extraData["identity"].ToString();
                    loginData.IdentityPublicKey = jwt["identityPublicKey"].ToString();
                }
            }

            int clientDataLen = stream.ReadInt();
            string clientDataJson = Encoding.UTF8.GetString(stream.ReadBytes(clientDataLen));
            JObject clientDataJwt = JObject.Parse(JWT.Decode(clientDataJson));
            //TODO: ClientDataJWT
            Logger.Log(clientDataJwt.ToString());
        }
    }
}
