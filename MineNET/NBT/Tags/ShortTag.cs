﻿using System;
using MineNET.NBT.Data;
using MineNET.NBT.IO;

namespace MineNET.NBT.Tags
{
    public class ShortTag : DataTag<short>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.SHORT;
            }
        }

        public ShortTag(short data) : this("", data)
        {

        }

        public ShortTag(string name, short data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"ShortTag : Name {this.Name} : Data {this.Data}";
        }

        internal override void Write(NBTStream stream)
        {
            stream.WriteShort(this.Data);
        }

        internal override void WriteTag(NBTStream stream)
        {
            if (this.Name != null)
            {
                stream.WriteByte((byte) this.TagType);
                stream.WriteString(this.Name);
                this.Write(stream);
            }
            else
            {
                throw new NullReferenceException("Tag Name Null");
            }
        }

        internal override void Read(NBTStream stream)
        {
            this.Data = stream.ReadShort();
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            this.Name = stream.ReadString();
            this.Read(stream);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ShortTag))
            {
                return false;
            }
            ShortTag tag = (ShortTag) obj;
            if (this.Name != tag.Name)
            {
                return false;
            }
            if (this.Data != tag.Data)
            {
                return false;
            }
            return true;
        }

        public static bool operator ==(ShortTag A, ShortTag B)
        {
            if (object.ReferenceEquals(A, B))
            {
                return true;
            }
            if ((object) A == null || (object) B == null)
            {
                return false;
            }
            return A.Equals(B);
        }

        public static bool operator !=(ShortTag A, ShortTag B)
        {
            if (object.ReferenceEquals(A, B))
            {
                return false;
            }
            if ((object) A == null || (object) B == null)
            {
                return true;
            }
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
