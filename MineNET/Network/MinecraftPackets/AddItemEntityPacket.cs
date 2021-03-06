﻿using MineNET.Entities.Metadata;
using MineNET.Items;
using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class AddItemEntityPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ADD_ITEM_ENTITY_PACKET;

        public long EntityUniqueId { get; set; }
        public long EntityRuntimeId { get; set; }
        public Item Item { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Motion { get; set; }
        public EntityMetadataManager Metadata { get; set; }
        public bool IsFromFishing { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteItem(this.Item);
            this.WriteVector3(this.Position);
            this.WriteVector3(this.Motion);
            this.WriteEntityMetadata(this.Metadata);
            this.WriteBool(this.IsFromFishing);
        }

        protected override void DecodePayload()
        {

        }
    }
}
