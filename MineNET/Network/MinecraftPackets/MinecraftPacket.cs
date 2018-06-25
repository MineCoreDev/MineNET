﻿using MineNET.Blocks;
using MineNET.Data;
using MineNET.Entities.Attributes;
using MineNET.Entities.Metadata;
using MineNET.Items;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds.Rule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MineNET.Network.MinecraftPackets
{
    public abstract class MinecraftPacket : BinaryStream, ICloneable<MinecraftPacket>
    {
        public abstract byte PacketID { get; }

        public byte Extra1 { get; set; }
        public byte Extra2 { get; set; }

        public virtual void Encode()
        {
            this.WriteByte(this.PacketID);
            this.WriteByte(this.Extra1);
            this.WriteByte(this.Extra2);
        }

        public virtual void Decode()
        {
            this.ReadByte();
            this.Extra1 = this.ReadByte();
            this.Extra2 = this.ReadByte();
        }

        public new MinecraftPacket Clone()
        {
            return (MinecraftPacket) this.MemberwiseClone();
        }

        public Vector2 ReadVector2()
        {
            return new Vector2(this.ReadLFloat(), this.ReadLFloat());
        }

        public void WriteVector2(Vector2 value)
        {
            this.WriteLFloat(value.X);
            this.WriteLFloat(value.Y);
        }

        public Vector3 ReadVector3()
        {
            return new Vector3(this.ReadLFloat(), this.ReadLFloat(), this.ReadLFloat());
        }

        public void WriteVector3(Vector3 value)
        {
            this.WriteLFloat(value.X);
            this.WriteLFloat(value.Y);
            this.WriteLFloat(value.Z);
        }

        public Vector3 ReadBlockVector3()
        {
            return new Vector3(
                this.ReadSVarInt(),
                this.ReadUVarInt(),
                this.ReadSVarInt()
            );
        }

        public void WriteBlockVector3(Vector3 pos)
        {
            this.WriteBlockVector3(pos.FloorX, pos.FloorY, pos.FloorZ);
        }

        public void WriteBlockVector3(int x, int y, int z)
        {
            this.WriteSVarInt(x);
            this.WriteUVarInt((uint) y);
            this.WriteSVarInt(z);
        }

        public Vector3 ReadSBlockVector3()
        {
            return new Vector3(
                this.ReadSVarInt(),
                this.ReadSVarInt(),
                this.ReadSVarInt()
            );
        }

        public void WriteSBlockVector3(Vector3 pos)
        {
            this.WriteSBlockVector3(pos.FloorX, pos.FloorY, pos.FloorZ);
        }

        public void WriteSBlockVector3(int x, int y, int z)
        {
            this.WriteSVarInt(x);
            this.WriteSVarInt(y);
            this.WriteSVarInt(z);
        }

        public byte[] ReadByteData()
        {
            int len = (int) this.ReadUVarInt();
            return this.ReadBytes(len);
        }

        public void WriteByteData(byte[] data)
        {
            this.WriteUVarInt((uint) data.Length);
            this.WriteBytes(data);
        }

        public long ReadEntityUniqueId()
        {
            return this.ReadSVarLong();
        }

        public void WriteEntityUniqueId(long eid)
        {
            this.WriteSVarLong(eid);
        }

        public long ReadEntityRuntimeId()
        {
            return (long) this.ReadUVarLong();
        }

        public void WriteEntityRuntimeId(long eid)
        {
            this.WriteUVarLong((ulong) eid);
        }

        public void WriteAttributes(EntityAttributeDictionary dictionary)
        {
            EntityAttribute[] attributes = dictionary.ToArray;
            this.WriteUVarInt((uint) attributes.Length);
            for (int i = 0; i < attributes.Length; ++i)
            {
                this.WriteLFloat(attributes[i].MinValue);
                this.WriteLFloat(attributes[i].MaxValue);
                this.WriteLFloat(attributes[i].Value);
                this.WriteLFloat(attributes[i].DefaultValue);
                this.WriteString(attributes[i].Name);
            }
        }

        public void WriteGameRules(GameRules rules)
        {
            if (rules == null)
            {
                this.WriteVarInt(0);
                return;
            }

            this.WriteVarInt(rules.Count);
            for (int i = 0; i < rules.Count; ++i)
            {
                this.WriteString(rules[i].Name.ToLower());
                if (rules[i] is GameRule<bool>)
                {
                    GameRule<bool> boolRule = (GameRule<bool>) rules[i];
                    this.WriteByte(1);
                    this.WriteBool(boolRule.Value);
                }
                else if (rules[i] is GameRule<int>)
                {
                    GameRule<int> intRule = (GameRule<int>) rules[i];
                    this.WriteByte(2);
                    this.WriteVarInt(intRule.Value);
                }
                else if (rules[i] is GameRule<float>)
                {
                    GameRule<float> floatValue = (GameRule<float>) rules[i];
                    this.WriteByte(3);
                    this.WriteLFloat(floatValue.Value);
                }
            }
        }

        /*public void WritePlayerListEntries(PlayerListEntry[] entries, byte type)
        {
            this.WriteByte(type);
            this.WriteUVarInt((uint) entries.Length);
            for (int i = 0; i < entries.Length; ++i)
            {
                this.WriteUUID(entries[i].UUID);
                if (type == PlayerListPacket.TYPE_ADD)
                {
                    this.WriteEntityUniqueId(entries[i].EntityUniqueId);
                    this.WriteString(entries[i].Name);
                    this.WriteString(entries[i].Name);
                    this.WriteSVarInt(entries[i].PlatForm);
                    this.WriteSkin(entries[i].Skin);
                    this.WriteString(entries[i].XboxUserId);
                    this.WriteString("");
                }
            }
        }*/

        public Skin ReadSkin()
        {
            return new Skin(this.ReadString(), this.ReadByteData(), this.ReadByteData(), this.ReadString(), this.ReadString());
        }

        public void WriteSkin(Skin skin)
        {
            this.WriteString(skin.SkinId);
            this.WriteByteData(skin.SkinData);
            this.WriteByteData(skin.CapeData);
            this.WriteString(skin.GeometryName);
            this.WriteString(skin.GeometryData);
        }

        public ItemStack ReadItem()
        {
            int id = this.ReadSVarInt();
            if (id == 0)
            {
                return new ItemStack(Item.Get(BlockIDs.AIR), 0, 0);
            }
            int auxValue = this.ReadSVarInt();
            int data = auxValue >> 8;
            if (data == short.MaxValue)
            {
                data = -1;
            }
            int cnt = auxValue & 0xff;

            int nbtLen = this.ReadLShort();
            byte[] nbt = new byte[0];
            if (nbtLen > 0)
            {
                nbt = this.ReadBytes(nbtLen);
            }

            ItemStack item = new ItemStack(Item.Get(id), data, cnt, nbt);

            int canPlaceOn = this.ReadSVarInt();
            if (canPlaceOn > 0)
            {
                for (int i = 0; i < canPlaceOn; ++i)
                {
                    item.AddCanPlaceOn(this.ReadString());
                }
            }

            int canDestroy = this.ReadSVarInt();
            if (canDestroy > 0)
            {
                for (int i = 0; i < canDestroy; ++i)
                {
                    item.AddCanDestroy(this.ReadString());
                }
            }

            return item;
        }

        public void WriteItem(ItemStack item)
        {
            int id = item.Item.ID;
            if (item == null || id == 0)
            {
                this.WriteSVarInt(0);
                return;
            }
            this.WriteSVarInt(id);
            int auxValue = ((item.Damage & 0x7fff) << 8) | (item.Count & 0xff);
            this.WriteSVarInt(auxValue);
            byte[] nbt = item.BinaryTags;
            this.WriteLShort((ushort) nbt.Length);
            this.WriteBytes(nbt);

            string[] canPlaceOn = item.CanPlaceOn;
            this.WriteSVarInt(canPlaceOn.Length);
            for (int i = 0; i < canPlaceOn.Length; ++i)
            {
                this.WriteString(canPlaceOn[i]);
            }

            string[] canDestroy = item.CanDestroy;
            this.WriteSVarInt(canDestroy.Length);
            for (int i = 0; i < canDestroy.Length; ++i)
            {
                this.WriteString(canDestroy[i]);
            }
        }

        //ReadEntityMetadata

        public void WriteEntityMetadata(EntityMetadataManager data)
        {
            Dictionary<int, EntityData> entityDatas = data.GetEntityDatas();
            this.WriteUVarInt((uint) entityDatas.Count);
            int[] keys = entityDatas.Keys.ToArray();
            for (int i = 0; i < keys.Length; ++i)
            {
                int id = keys[i];
                EntityData entityData = entityDatas[id];
                EntityMetadataType type = entityData.Type;
                this.WriteUVarInt((uint) id);
                this.WriteUVarInt((uint) type);
                if (type == EntityMetadataType.DATA_TYPE_BYTE)
                {
                    this.WriteByte(data.GetByte(id));
                }
                else if (type == EntityMetadataType.DATA_TYPE_SHORT)
                {
                    this.WriteLShort((ushort) data.GetShort(id));
                }
                else if (type == EntityMetadataType.DATA_TYPE_INT)
                {
                    this.WriteSVarInt(data.GetInt(id));
                }
                else if (type == EntityMetadataType.DATA_TYPE_FLOAT)
                {
                    this.WriteLFloat(data.GetFloat(id));
                }
                else if (type == EntityMetadataType.DATA_TYPE_STRING)
                {
                    this.WriteString(data.GetString(id));
                }
                else if (type == EntityMetadataType.DATA_TYPE_SLOT)
                {
                    this.WriteItem(data.GetSlot(id));
                }
                else if (type == EntityMetadataType.DATA_TYPE_LONG)
                {
                    this.WriteSVarLong(data.GetLong(id));
                }
                else if (type == EntityMetadataType.DATA_TYPE_VECTOR)
                {
                    this.WriteVector3(data.GetVector(id));
                }
            }
        }

        /*public CommandOriginData ReadCommandOriginData()
        {
            CommandOriginData commandOriginData = new CommandOriginData();
            commandOriginData.Type = this.ReadUVarInt();
            commandOriginData.Uuid = this.ReadUUID();
            commandOriginData.RequestId = this.ReadString();
            if (commandOriginData.Type == CommandOriginData.ORIGIN_DEV_CONSOLE || commandOriginData.Type == CommandOriginData.ORIGIN_TEST)
            {
                commandOriginData.Unknown = this.ReadVarLong();
            }
            return commandOriginData;
        }

        public BlockFace ReadBlockFace()
        {
            return BlockFaceExtensions.FromIndex(this.ReadSVarInt());
        }

        public void WriteBlockFace(BlockFace face)
        {
            this.WriteSVarInt(face.GetIndex());
        }*/

        public UUID ReadUUID()
        {
            return new UUID(this.ReadBytes(16));
        }

        public void WriteUUID(UUID uuid)
        {
            this.WriteBytes(uuid.GetBytes());
        }
    }
}
