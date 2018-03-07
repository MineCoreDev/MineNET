using System.Collections.Generic;
using MineNET.Data;
using MineNET.Entities.Attributes;
using MineNET.Entities.Data;
using MineNET.Entities.Metadata;
using MineNET.Items;
using MineNET.Network.Packets;
using MineNET.Values;
using MineNET.Worlds.Data;

namespace MineNET.Utils
{
    public class MCBEBinary : BinaryStream
    {
        public Vector2 ReadVector2()
        {
            return new Vector2(this.ReadFloat(), this.ReadFloat());
        }

        public void WriteVector2(Vector2 value)
        {
            this.WriteFloat(value.X);
            this.WriteFloat(value.Y);
        }

        public Vector3 ReadVector3()
        {
            return new Vector3(this.ReadFloat(), this.ReadFloat(), this.ReadFloat());
        }

        public void WriteVector3(Vector3 value)
        {
            this.WriteFloat(value.X);
            this.WriteFloat(value.Y);
            this.WriteFloat(value.Z);
        }

        //TODO : ReadBlockPosition

        public void WriteBlockPosition(Vector3 pos)
        {
            this.WriteBlockPosition(pos.ToVector3i());
        }

        public void WriteBlockPosition(Vector3i pos)
        {
            this.WriteBlockPosition(pos.X, pos.Y, pos.Z);
        }

        public void WriteBlockPosition(int x, int y, int z)
        {
            this.WriteSVarInt(x);
            this.WriteUVarInt((uint) y);
            this.WriteSVarInt(z);
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

        public void WriteAttributes(params EntityAttribute[] attributes)
        {
            this.WriteUVarInt((uint) attributes.Length);
            for (int i = 0; i < attributes.Length; ++i)
            {
                this.WriteFloat(attributes[i].MinValue);
                this.WriteFloat(attributes[i].MaxValue);
                this.WriteFloat(attributes[i].Value);
                this.WriteFloat(attributes[i].DefaultValue);
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
                    this.WriteFloat(floatValue.Value);
                }
            }
        }

        public void WritePlayerListEntries(PlayerListEntry[] entries, byte type)
        {
            this.WriteByte(type);
            this.WriteUVarLong((uint) entries.Length);
            for (int i = 0; i < entries.Length; ++i)
            {
                this.WriteGUID(entries[i].Guid);
                if (type == PlayerListPacket.TYPE_ADD)
                {
                    this.WriteEntityUniqueId(entries[i].EntityUniqueId);
                    this.WriteString(entries[i].Name);
                    this.WriteSkin(entries[i].Skin);
                    this.WriteString(entries[i].XboxUserId);
                }
            }
        }

        public Skin ReadSkin()
        {
            return new Skin(this.ReadString(), this.ReadBytes((int) this.ReadUVarInt()), this.ReadBytes((int) this.ReadUVarInt()), this.ReadString(), this.ReadString());
        }

        public void WriteSkin(Skin skin)
        {
            /*this.WriteString(skin.SkinId);
            this.WriteString(Convert.ToBase64String(skin.SkinData));
            this.WriteString(Convert.ToBase64String(skin.CapeData));

            this.WriteString(skin.GeometryName);
            this.WriteString(skin.GeometryData);*/
            this.WriteString(skin.SkinId);
            this.WriteUVarInt(0);
            this.WriteUVarInt(0);

            this.WriteString(skin.GeometryName);
            this.WriteUVarInt(0);
        }

        public void WrileByteAndLen(byte[] buffer)
        {
            int len = buffer.Length;
            this.WriteUVarInt((uint) len);
            this.WriteBytes(buffer);
        }

        public Item ReadItem()
        {
            int id = this.ReadVarInt();
            if (id < 0)
            {
                return Item.Get(0, 0, 0);
            }
            int auxValue = this.ReadVarInt();
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

            //TODO
            int canPlaceOn = this.ReadVarInt();
            if (canPlaceOn > 0)
            {
                for (int i = 0; i < canPlaceOn; ++i)
                {
                    this.ReadString();
                }
            }

            //TODO
            int canDestroy = this.ReadVarInt();
            if (canDestroy > 0)
            {
                for (int i = 0; i < canDestroy; ++i)
                {
                    this.ReadString();
                }
            }
            return Item.Get(id, data, cnt, nbt);
        }

        public void WriteItem(Item item)
        {
            if (item == null || item.ID == 0)
            {
                this.WriteVarInt(0);
                return;
            }
            this.WriteVarInt(item.ID);
            int auxValue = (((item.Damage != 0 ? item.Damage : -1) & 0x7fff) << 8) | item.Count;
            this.WriteVarInt(auxValue);
            byte[] nbt = item.Tags;
            this.WriteLShort((ushort) nbt.Length);
            this.WriteBytes(nbt);
            this.WriteVarInt(0); //TODO
            this.WriteVarInt(0); //TODO
        }

        //ReadEntityMetadata

        public void WriteEntityMetadata(EntityMetadataManager data)
        {
            using (MCBEBinary stream = new MCBEBinary())
            {
                Dictionary<int, EntityData> entityDatas = data.GetEntityDatas();
                stream.WriteUVarInt((uint) entityDatas.Count);
                foreach (int id in entityDatas.Keys)
                {
                    EntityData entityData = entityDatas[id];
                    EntityMetadataType type = entityData.Type;
                    stream.WriteUVarInt((uint) id);
                    stream.WriteUVarInt((uint) type);
                    if (type == EntityMetadataType.DATA_TYPE_BYTE)
                    {
                        stream.WriteByte(data.GetByte(id));
                    }
                    else if (type == EntityMetadataType.DATA_TYPE_SHORT)
                    {
                        stream.WriteLShort((ushort) data.GetShort(id));
                    }
                    else if (type == EntityMetadataType.DATA_TYPE_INT)
                    {
                        stream.WriteSVarInt(data.GetInt(id));
                    }
                    else if (type == EntityMetadataType.DATA_TYPE_FLOAT)
                    {
                        stream.WriteLFloat(data.GetFloat(id));
                    }
                    else if (type == EntityMetadataType.DATA_TYPE_STRING)
                    {
                        stream.WriteString(data.GetString(id));
                    }
                    else if (type == EntityMetadataType.DATA_TYPE_SLOT)
                    {
                        stream.WriteItem(data.GetSlot(id));
                    }
                    else if (type == EntityMetadataType.DATA_TYPE_LONG)
                    {
                        stream.WriteSVarLong(data.GetLong(id));
                    }
                    else if (type == EntityMetadataType.DATA_TYPE_VECTOR)
                    {
                        stream.WriteVector3(data.GetVector(id));
                    }
                }
                this.WriteBytes(stream.GetResult());
            }
        }
    }
}
