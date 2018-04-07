using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;

namespace MineNET.GUI.Forms
{
    public partial class NBTViewer : Form
    {
        public NBTViewer()
        {
            this.InitializeComponent();
        }

        public NBTViewer(CompoundTag tag)
        {
            this.InitializeComponent();
            this.LoadTag(tag);
        }

        private void LoadTag(CompoundTag tags)
        {
            foreach (KeyValuePair<string, Tag> tagKV in tags.Tags)
            {
                Tag tag = tagKV.Value;
                if (tag is ByteTag)
                {
                    ByteTag t = (ByteTag) tag;
                    this.AddTag<byte>(t.Name, t.Data, t.TagType);
                }
                //TODO:
            }
        }

        private CompoundTag SaveTag()
        {
            throw new NotImplementedException();
            //TODO:
            //this.dataGridView1.Rows[0].Cells[0] 
        }

        private void AddTag<T>(string key, T value, NBTTagType type)
        {
            this.dataGridView1.Rows.Add(key, value, type.ToNameString());
        }
    }
}
