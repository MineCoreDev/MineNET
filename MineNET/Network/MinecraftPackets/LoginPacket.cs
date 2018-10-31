using System;
using System.Linq;
using System.Text;
using MineNET.Data;
using MineNET.Utils;
using MineNET.Values;
using Newtonsoft.Json.Linq;

namespace MineNET.Network.MinecraftPackets
{
    public class LoginPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.LOGIN_PACKET;

        public int Protocol { get; set; }
        public LoginData LoginData { get; set; }
        public ClientData ClientData { get; set; }

        public bool Result { get; private set; } = true;

        protected override void EncodePayload()
        {

        }

        protected override void DecodePayload()
        {
            this.Protocol = this.ReadInt();

            this.LoginData = new LoginData();
            this.ClientData = new ClientData();

            int len = this.ReadVarInt();
            using (BinaryStream stream = new BinaryStream(this.ReadBytes(len)))
            {
                try
                {
                    int chainLen = (int) stream.ReadLInt();
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
                            this.LoginData.ClientUUID = new UUID(extraData.Value<string>("identity"));
                            this.LoginData.IdentityPublicKey = jwt.Value<string>("identityPublicKey");
                        }
                    }

                    int clientDataLen = (int) stream.ReadLInt();
                    string clientDataJson = Encoding.UTF8.GetString(stream.ReadBytes(clientDataLen));

                    this.SetClientData(clientDataJson);
                }
                catch
                {
                    this.Result = false;
                }
            }
        }

        private void SetClientData(string json)
        {
            JObject clientDataJwt = JObject.Parse(JWT.Decode(json));

            this.ClientData.ClientRandomID = clientDataJwt.Value<string>("ClientRandomId");
            this.ClientData.CurrentInputMode = clientDataJwt.Value<int>("CurrentInputMode");
            this.ClientData.DefaultInputMode = clientDataJwt.Value<int>("DefaultInputMode");
            this.ClientData.DeviceModel = clientDataJwt.Value<string>("DeviceModel");
            this.ClientData.DeviceOS = clientDataJwt.Value<int>("DeviceOS");
            this.ClientData.GameVersion = clientDataJwt.Value<string>("GameVersion");
            this.ClientData.GUIScale = clientDataJwt.Value<int>("GuiScale");
            this.ClientData.LanguageCode = clientDataJwt.Value<string>("LanguageCode");
            this.ClientData.ServerAddress = clientDataJwt.Value<string>("ServerAddress");
            this.ClientData.Skin = new Skin(
                clientDataJwt.Value<string>("SkinId"),
                Convert.FromBase64String(clientDataJwt.Value<string>("SkinData")),
                Convert.FromBase64String(clientDataJwt.Value<string>("CapeData")),
                clientDataJwt.Value<string>("SkinGeometryName"),
                Encoding.UTF8.GetString(Convert.FromBase64String(clientDataJwt.Value<string>("SkinGeometry")))
            );
            this.ClientData.UIProfile = clientDataJwt.Value<int>("UIProfile");
        }

        public override void Dispose()
        {
            base.Dispose();

            this.ClientData = null;
            this.LoginData = null;
        }
    }
}