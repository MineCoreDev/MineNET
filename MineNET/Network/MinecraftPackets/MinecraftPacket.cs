using System;
using System.Collections.Generic;
using System.Linq;
using MineNET.Blocks;
using MineNET.Commands.Enums;
using MineNET.Data;
using MineNET.Entities.Attributes;
using MineNET.Entities.Metadata;
using MineNET.Items;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds.Rule;

namespace MineNET.Network.MinecraftPackets
{
    public abstract class MinecraftPacket : BinaryStream, ICloneable<MinecraftPacket>
    {
        public const int CHANNEL_NONE = 0;
        public const int CHANNEL_IMMEDIATE = 1;
        public const int CHANNEL_CHUNK = 2;

        public abstract byte PacketID { get; }
        public virtual int OrderChannel => CHANNEL_NONE;

        public void Encode()
        {
            this.WriteByte(this.PacketID);
            this.EncodePayload();
        }

        protected abstract void EncodePayload();

        public void Decode()
        {
            this.ReadByte();
            this.DecodePayload();
        }

        protected abstract void DecodePayload();

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

        public BlockCoordinate3D ReadBlockVector3()
        {
            return new BlockCoordinate3D(
                this.ReadSVarInt(),
                (int) this.ReadUVarInt(),
                this.ReadSVarInt()
            );
        }

        public void WriteBlockVector3(BlockCoordinate3D pos)
        {
            this.WriteBlockVector3(pos.X, pos.Y, pos.Z);
        }

        public void WriteBlockVector3(int x, int y, int z)
        {
            this.WriteSVarInt(x);
            this.WriteUVarInt((uint) y);
            this.WriteSVarInt(z);
        }

        public BlockCoordinate3D ReadSBlockVector3()
        {
            return new BlockCoordinate3D(
                this.ReadSVarInt(),
                this.ReadSVarInt(),
                this.ReadSVarInt()
            );
        }

        public void WriteSBlockVector3(BlockCoordinate3D pos)
        {
            this.WriteSBlockVector3(pos.X, pos.Y, pos.Z);
        }

        public void WriteSBlockVector3(int x, int y, int z)
        {
            this.WriteSVarInt(x);
            this.WriteSVarInt(y);
            this.WriteSVarInt(z);
        }

        public float ReadByteRotation()
        {
            return this.ReadByte() * (360 / 256);
        }

        public void WriteByteRotation(float rotation)
        {
            this.WriteByte((byte) (rotation / (360 / 256)));
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

        public void WritePlayerListEntries(PlayerListEntry[] entries, byte type)
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
                    this.WriteSkin(entries[i].Skin);
                    this.WriteString(entries[i].XboxUserId);
                    this.WriteString("");
                }
            }
        }

        public Skin ReadSkin()
        {
            return new Skin(this.ReadString(), this.ReadByteData(), this.ReadByteData(), this.ReadString(),
                this.ReadString());
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

        public CommandOriginData ReadCommandOriginData()
        {
            CommandOriginData commandOriginData = new CommandOriginData();
            commandOriginData.Type = this.ReadUVarInt();
            commandOriginData.Uuid = this.ReadUUID();
            commandOriginData.RequestId = this.ReadString();
            if (commandOriginData.Type == CommandOriginData.ORIGIN_DEV_CONSOLE ||
                commandOriginData.Type == CommandOriginData.ORIGIN_TEST)
            {
                commandOriginData.VarLong1 = this.ReadVarLong();
            }

            return commandOriginData;
        }

        public void WriteCommandOriginData(CommandOriginData commandOriginData)
        {
            this.WriteUVarInt(commandOriginData.Type);
            this.WriteUUID(commandOriginData.Uuid);
            this.WriteString(commandOriginData.RequestId);
            if (commandOriginData.Type == CommandOriginData.ORIGIN_DEV_CONSOLE ||
                commandOriginData.Type == CommandOriginData.ORIGIN_TEST)
            {
                this.WriteVarLong(commandOriginData.VarLong1);
            }
        }

        public CommandOutputMessage ReadCommandOutputMessage()
        {
            CommandOutputMessage message = new CommandOutputMessage();
            message.IsInternal = this.ReadBool();
            message.MessageId = this.ReadString();
            message.Parameters = new string[this.ReadUVarInt()];
            for (int i = 0; i < message.Parameters.Length; ++i)
            {
                message.Parameters[i] = this.ReadString();
            }

            return message;
        }

        public void WriteCommandOutputMessage(CommandOutputMessage message)
        {
            this.WriteBool(message.IsInternal);
            this.WriteString(message.MessageId);
            this.WriteUVarInt((uint) message.Parameters.Length);
            for (int i = 0; i < message.Parameters.Length; ++i)
            {
                this.WriteString(message.Parameters[i]);
            }
        }

        public void WriteEnumValues(List<string> enumValues)
        {
            this.WriteUVarInt((uint) enumValues.Count);
            for (int i = 0; i < enumValues.Count; ++i)
            {
                this.WriteString(enumValues[i]);
            }
        }

        public void WritePostfixes(List<string> postFixes)
        {
            this.WriteUVarInt((uint) postFixes.Count);
            for (int i = 0; i < postFixes.Count; ++i)
            {
                this.WriteString(postFixes[i]);
            }
        }

        public void WriteEnums(List<CommandEnum> enums, List<string> enumValues)
        {
            this.WriteUVarInt((uint) enums.Count);
            for (int i = 0; i < enums.Count; ++i)
            {
                CommandEnum enumData = enums[i];
                int count = enumData.Values.Length;
                string name = enumData.Name;
                if (string.IsNullOrEmpty(name))
                {
                    continue;
                }

                this.WriteString(name);
                this.WriteUVarInt((uint) count);
                for (int j = 0; j < count; ++j)
                {
                    if (enumValues.Count < 0x100)
                    {
                        this.WriteByte((byte) enumValues.IndexOf(enumData.Values[j]));
                    }
                    else if (enumValues.Count < 0x10000)
                    {
                        this.WriteLShort((ushort) enumValues.IndexOf(enumData.Values[j]));
                    }
                    else
                    {
                        this.WriteLInt((uint) enumValues.IndexOf(enumData.Values[j]));
                    }
                }
            }
        }

        public void WriteSoftEnums(Dictionary<string, List<string>> softEnums)
        {
            this.WriteUVarInt((uint) softEnums.Count);
            foreach (KeyValuePair<string, List<string>> softEnum in softEnums)
            {
                int count = softEnum.Value.Count;
                this.WriteString(softEnum.Key);
                this.WriteUVarInt((uint) count);
                for (int i = 0; i < count; ++i)
                {
                    this.WriteString(softEnum.Value[i]);
                }
            }
        }

        public BlockFace ReadBlockFace()
        {
            return BlockFaceExtensions.FromIndex(this.ReadSVarInt());
        }

        public void WriteBlockFace(BlockFace face)
        {
            this.WriteSVarInt(face.GetIndex());
        }

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