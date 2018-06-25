﻿using System;
using MineNET.NBT.Data;
using MineNET.NBT.IO;

namespace MineNET.NBT.Tags
{
    public class IntTag : DataTag<int>
    {
        public override NBTTagType TagType
        {
            get
            {
                return NBTTagType.INT;
            }
        }

        public IntTag(int data) : this("", data)
        {

        }

        public IntTag(string name, int data) : base(name, data)
        {

        }

        public override string ToString()
        {
            return $"IntTag : Name {this.Name} : Data {this.Data}";
        }

        internal override void Write(NBTStream stream)
        {
            stream.WriteInt(this.Data);
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
            this.Data = stream.ReadInt();
        }

        internal override void ReadTag(NBTStream stream)
        {
            stream.ReadByte();
            this.Name = stream.ReadString();
            this.Read(stream);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IntTag))
            {
                return false;
            }
            IntTag tag = (IntTag) obj;
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

        public static bool operator ==(IntTag A, IntTag B)
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

        public static bool operator !=(IntTag A, IntTag B)
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
