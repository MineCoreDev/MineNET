using System;
using System.Collections.Generic;
using System.Drawing;
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
            this.LoadTag(tag, true);
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
            {
                SendKeys.Send("{F4}");
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string type = (string) this.dataGridView1.Rows[e.RowIndex].Cells[2].Value;
            string value = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();

            if (e.ColumnIndex == 1)
            {
                value = e.FormattedValue?.ToString();
            }
            else if (e.ColumnIndex == 2)
            {
                type = e.FormattedValue.ToString();
            }

            if ((type == NBTTagType.LIST.ToNameString() || type == NBTTagType.COMPOUND.ToNameString() || type == NBTTagType.END.ToNameString()))
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = "";
                return;
            }

            if (value == null || string.IsNullOrWhiteSpace(value))
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = "値が null です。";
                return;
            }

            if (type == NBTTagType.BYTE.ToNameString())
            {
                byte check;
                if (byte.TryParse(value, out check))
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = "";
                }
                else
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = string.Format("型 <{0}> の範囲を超えています。範囲<{1} ～ {2}>", type, byte.MinValue, byte.MaxValue);
                }
            }
            else if (type == NBTTagType.SHORT.ToNameString())
            {
                short check;
                if (short.TryParse(value, out check))
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = "";
                }
                else
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = string.Format("型 <{0}> の範囲を超えています。範囲<{1} ～ {2}>", type, short.MinValue, short.MaxValue);
                }
            }
            else if (type == NBTTagType.INT.ToNameString())
            {
                int check;
                if (int.TryParse(value, out check))
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = "";
                }
                else
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = string.Format("型 <{0}> の範囲を超えています。範囲<{1} ～ {2}>", type, int.MinValue, int.MaxValue);
                }
            }
            else if (type == NBTTagType.LONG.ToNameString())
            {
                long check;
                if (long.TryParse(value, out check))
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = "";
                }
                else
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = string.Format("型 <{0}> の範囲を超えています。範囲<{1} ～ {2}>", type, long.MinValue, long.MaxValue);
                }
            }
            else if (type == NBTTagType.FLOAT.ToNameString())
            {
                float check;
                if (float.TryParse(value, out check))
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = "";
                }
                else
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = string.Format("型 <{0}> の範囲を超えています。範囲<{1} ～ {2}>", type, float.MinValue, float.MaxValue);
                }
            }
            else if (type == NBTTagType.DOUBLE.ToNameString())
            {
                double check;
                if (double.TryParse(value, out check))
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = "";
                }
                else
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = string.Format("型 <{0}> の範囲を超えています。範囲<{1} ～ {2}>", type, double.MinValue, double.MaxValue);
                }
            }
            else
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[1].ErrorText = "TagType が不正です。";
            }
        }

        private void dataGridView1_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.Value.ToString() == NBTTagType.COMPOUND.ToNameString())
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].ReadOnly = false;
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle();
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = null;
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].Style = this.ReadOnlyCellStyle;
                }
                else if (e.Value.ToString() == NBTTagType.LIST.ToNameString())
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = null;
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].Style = this.ReadOnlyCellStyle;
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ReadOnly = false;
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].Style = new DataGridViewCellStyle();
                }
                else if (e.Value.ToString() == NBTTagType.END.ToNameString())
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = null;
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].Style = this.ReadOnlyCellStyle;
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = null;
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].Style = this.ReadOnlyCellStyle;
                }
                else
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].ReadOnly = false;
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle();
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].ReadOnly = false;
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].Style = new DataGridViewCellStyle();
                }
            }
        }

        private void LoadTag(CompoundTag tags, bool isRoot = false)
        {
            if (!isRoot)
            {
                this.AddCompoundTagHeader(tags);
            }

            foreach (KeyValuePair<string, Tag> tagKV in tags.Tags)
            {
                Tag tag = tagKV.Value;
                if (tag is EndTag)
                {
                    this.AddEndTag();
                }
                else if (tag is ByteTag)
                {
                    ByteTag t = (ByteTag) tag;
                    this.AddTag(t.Name, t.Data, t.TagType);
                }
                else if (tag is ShortTag)
                {
                    ShortTag t = (ShortTag) tag;
                    this.AddTag(t.Name, t.Data, t.TagType);
                }
                else if (tag is IntTag)
                {
                    IntTag t = (IntTag) tag;
                    this.AddTag(t.Name, t.Data, t.TagType);
                }
                else if (tag is LongTag)
                {
                    LongTag t = (LongTag) tag;
                    this.AddTag(t.Name, t.Data, t.TagType);
                }
                else if (tag is FloatTag)
                {
                    FloatTag t = (FloatTag) tag;
                    this.AddTag(t.Name, t.Data, t.TagType);
                }
                else if (tag is DoubleTag)
                {
                    DoubleTag t = (DoubleTag) tag;
                    this.AddTag(t.Name, t.Data, t.TagType);
                }
                //TODO: ByteArrayTag...
                else if (tag is StringTag)
                {
                    StringTag t = (StringTag) tag;
                    this.AddTag(t.Name, t.Data, t.TagType);
                }
                else if (tag is ListTag)
                {

                }
                else if (tag is CompoundTag)
                {
                    CompoundTag t = (CompoundTag) tag;
                    this.LoadTag(t);
                }
                //TODO: IntArrayTag...
                //TDDO: LongArrayTag...
            }

            if (!isRoot)
            {
                this.AddEndTag();
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

        private void AddCompoundTagHeader(CompoundTag tag)
        {
            int index = this.dataGridView1.Rows.Add(tag.Name, null, "Compound");
            this.dataGridView1.Rows[index].Cells[1].ReadOnly = true;
            this.dataGridView1.Rows[index].Cells[1].Style = this.ReadOnlyCellStyle;
        }

        private void AddEndTag()
        {
            int index = this.dataGridView1.Rows.Add(null, null, "End");
            this.dataGridView1.Rows[index].Cells[0].ReadOnly = true;
            this.dataGridView1.Rows[index].Cells[0].Style = this.ReadOnlyCellStyle;
            this.dataGridView1.Rows[index].Cells[1].ReadOnly = true;
            this.dataGridView1.Rows[index].Cells[1].Style = this.ReadOnlyCellStyle;
        }

        private DataGridViewCellStyle ReadOnlyCellStyle
        {
            get
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.BackColor = Color.Gray;
                return style;
            }
        }

        private void moveUpUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MoveUpCell();
        }

        private void moveDownDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MoveDownCell();
        }

        public void MoveUpCell()
        {
            DataGridViewRow row = this.dataGridView1.CurrentRow;
            int index = this.dataGridView1.CurrentRow.Index;
            if (index != 0)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.Cells.AddRange(this.GetCellArray(row));
                this.dataGridView1.Rows.RemoveAt(index);
                this.dataGridView1.Rows.Insert(index - 1, newRow);
            }
        }

        public void MoveDownCell()
        {
            DataGridViewRow row = this.dataGridView1.CurrentRow;
            int index = this.dataGridView1.CurrentRow.Index;
            if (index < this.dataGridView1.Rows.Count)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.Cells.AddRange(this.GetCellArray(row));
                this.dataGridView1.Rows.RemoveAt(index);
                this.dataGridView1.Rows.Insert(index + 1, newRow);
            }
        }

        public DataGridViewCell[] GetCellArray(DataGridViewRow row)
        {
            DataGridViewCell[] cells = new DataGridViewCell[row.Cells.Count];
            row.Cells.CopyTo(cells, 0);
            return cells;
        }
    }
}
