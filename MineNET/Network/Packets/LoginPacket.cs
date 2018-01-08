using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using MineNET.Data;
using MineNET.Utils;


namespace MineNET.Network.Packets
{
    public class LoginPacket : Packet
    {
        public const byte NETWORK_ID = 0x01;

        private int protocol;
        private string chainData;

        private LoginExtraData extraData;
        private string identityPublicKey;

        private string clientDataJWT;

        public override byte ID
        {
            get
            {
                return NETWORK_ID;
            }
        }

        public override void Decode()
        {
            this.Reset();
            base.Decode();
            protocol = this.ReadInt();

            var bs = new BinaryStream(this.ReadPacketBuffer());
            bs.Position = 0;

            this.chainData = Encoding.UTF8.GetString(bs.ReadBytes(4, (int)bs.ReadLInt() + 4));
            var obj = JObject.Parse(this.chainData)["chain"];//TODO JWT Decode...
            for (int i = 0; i < obj.Count(); ++i)
            {
                var chain = JObject.Parse(JWT.Decode(obj[i].ToString()));
                JToken v = null;
                if (chain.TryGetValue("extraData", out v))
                {
                    extraData = new LoginExtraData()
                    {
                        DisplayName = v["displayName"].ToString(),
                        ClientUUID = v["identity"].ToString()
                    };

                    JToken identityPublicKeyJT = null;
                    chain.TryGetValue("identityPublicKey", out identityPublicKeyJT);
                    this.identityPublicKey = identityPublicKeyJT.ToString();
                }
            }

            this.clientDataJWT = Encoding.UTF8.GetString(bs.ReadBytes((int)bs.Position + 4, (int)bs.ReadLInt() + 4));
            Console.WriteLine(JObject.Parse(JWT.Decode(this.clientDataJWT)));//Test...
        }
    }
}
