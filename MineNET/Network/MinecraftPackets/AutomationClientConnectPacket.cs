﻿using System;

namespace MineNET.Network.MinecraftPackets
{
    public class AutomationClientConnectPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.AUTOMATION_CLIENT_CONNECT_PACKET;

        public String ServerUri { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.ServerUri);
        }

        protected override void DecodePayload()
        {
            this.ServerUri = this.ReadString();
        }
    }
}
