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
            using (BinaryStream stream = new BinaryStream(ReadBytes(len)))
            {

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
                        loginData.XUID = extraData.Value<string>("XUID");
                        loginData.DisplayName = extraData.Value<string>("displayName");
                        loginData.ClientUUID = extraData.Value<string>("identity");
                        loginData.IdentityPublicKey = jwt.Value<string>("identityPublicKey");
                    }
                }

                int clientDataLen = stream.ReadInt();
                string clientDataJson = Encoding.UTF8.GetString(stream.ReadBytes(clientDataLen));

                SetClientData(clientDataJson);
            }
        }

        void SetClientData(string json)
        {
            JObject clientDataJwt = JObject.Parse(JWT.Decode(json));

            clientData.CapeData = clientDataJwt.Value<string>("CapeData");
            clientData.ClientRandomID = clientDataJwt.Value<string>("ClientRandomId");
            clientData.CurrentInputMode = clientDataJwt.Value<int>("CurrentInputMode");
            clientData.DefaultInputMode = clientDataJwt.Value<int>("DefaultInputMode");
            clientData.DeviceModel = clientDataJwt.Value<string>("DeviceModel");
            clientData.DeviceOS = clientDataJwt.Value<int>("DeviceOS");
            clientData.GameVersion = clientDataJwt.Value<string>("GameVersion");
            clientData.GUIScale = clientDataJwt.Value<int>("GuiScale");
            clientData.LanguageCode = clientDataJwt.Value<string>("LanguageCode");
            clientData.ServerAddress = clientDataJwt.Value<string>("ServerAddress");
            clientData.SkinData = clientDataJwt.Value<string>("SkinData");
            clientData.SkinGeometry = clientDataJwt.Value<string>("SkinGeometry");
            clientData.SkinGeometryName = clientDataJwt.Value<string>("SkinGeometryName");
            clientData.SkinID = clientDataJwt.Value<string>("SkinId");
            clientData.UIProfile = clientDataJwt.Value<int>("UIProfile");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            clientData = null;
            LoginData = null;
        }
    }
}
