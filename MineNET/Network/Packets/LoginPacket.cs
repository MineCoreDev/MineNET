using MineNET.Data;
using MineNET.Utils;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;

namespace MineNET.Network.Packets
{
    public class LoginPacket : DataPacket
    {
        public const int ID = ProtocolInfo.LOGIN_PACKET;

        public override byte PacketID
        {
            get
            {
                return LoginPacket.ID;
            }
        }

        public int Protocol { get; set; }

        public LoginData LoginData { get; set; }

        public ClientData ClientData { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.Protocol = (int) this.ReadLInt();

            this.LoginData = new LoginData();
            this.ClientData = new ClientData();

            int len = this.ReadVarInt();
            using (BinaryStream stream = new BinaryStream(this.ReadBytes(len)))
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
                        this.LoginData.XUID = extraData.Value<string>("XUID");
                        this.LoginData.DisplayName = extraData.Value<string>("displayName");
                        this.LoginData.ClientUUID = extraData.Value<string>("identity");
                        this.LoginData.IdentityPublicKey = jwt.Value<string>("identityPublicKey");
                    }
                }

                int clientDataLen = stream.ReadInt();
                string clientDataJson = Encoding.UTF8.GetString(stream.ReadBytes(clientDataLen));

                this.SetClientData(clientDataJson);
            }
        }

        private void SetClientData(string json)
        {
            JObject clientDataJwt = JObject.Parse(JWT.Decode(json));

            this.ClientData.CapeData = clientDataJwt.Value<string>("CapeData");
            this.ClientData.ClientRandomID = clientDataJwt.Value<string>("ClientRandomId");
            this.ClientData.CurrentInputMode = clientDataJwt.Value<int>("CurrentInputMode");
            this.ClientData.DefaultInputMode = clientDataJwt.Value<int>("DefaultInputMode");
            this.ClientData.DeviceModel = clientDataJwt.Value<string>("DeviceModel");
            this.ClientData.DeviceOS = clientDataJwt.Value<int>("DeviceOS");
            this.ClientData.GameVersion = clientDataJwt.Value<string>("GameVersion");
            this.ClientData.GUIScale = clientDataJwt.Value<int>("GuiScale");
            this.ClientData.LanguageCode = clientDataJwt.Value<string>("LanguageCode");
            this.ClientData.ServerAddress = clientDataJwt.Value<string>("ServerAddress");
            this.ClientData.SkinData = clientDataJwt.Value<string>("SkinData");
            this.ClientData.SkinGeometry = clientDataJwt.Value<string>("SkinGeometry");
            this.ClientData.SkinGeometryName = clientDataJwt.Value<string>("SkinGeometryName");
            this.ClientData.SkinID = clientDataJwt.Value<string>("SkinId");
            this.ClientData.UIProfile = clientDataJwt.Value<int>("UIProfile");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            this.ClientData = null;
            this.LoginData = null;
        }
    }
}
