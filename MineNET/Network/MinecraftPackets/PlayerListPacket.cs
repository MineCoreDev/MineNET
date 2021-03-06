﻿using MineNET.Data;

namespace MineNET.Network.MinecraftPackets
{
    public class PlayerListPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.PLAYER_LIST_PACKET;

        public const byte TYPE_ADD = 0;
        public const byte TYPE_REMOVE = 1;

        public byte Type { get; set; }

        public PlayerListEntry[] Entries { get; set; }

        protected override void EncodePayload()
        {
            this.WritePlayerListEntries(this.Entries, this.Type);
        }

        protected override void DecodePayload()
        {

        }
    }
}
