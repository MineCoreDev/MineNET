﻿using MineNET.Values;

namespace MineNET.Data
{
    public sealed class LoginData
    {
        public string XUID { get; set; }
        public string DisplayName { get; set; }
        public UUID ClientUUID { get; set; }
        public string IdentityPublicKey { get; set; }
    }
}
