using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;

namespace MineNET.GUI.Forms
{
    public partial class NBTViewer : Form
    {
        private NBTEndian Endian { get; set; } = NBTEndian.LITTLE_ENDIAN;

        public NBTViewer()
        {
            this.InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = true;
        }

        public NBTViewer(CompoundTag tag)
        {
            this.InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = true;
            this._Load(tag);
        }

        private async void _Load(CompoundTag tag)
        {
            Task.Run(() =>
            {
                this.LoadTag(tag, true);
            }).Wait();
            await Task.Delay(1);
            this.UpdateRows();
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
            string type = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString();
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
                    ListTag t = (ListTag) tag;
                    this.AddListTagHeader(t);
                    //TODO Color(Random) ChangeStart...
                    //this.LoadTag(t);
                    //     Color(Random) ChengeEnd...
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
            this.cacheData.NBTViewerCache.AddNBTViewerCacheRow(key, value, type.ToNameString());
        }

        private void AddListTagHeader(ListTag tag)
        {
            this.cacheData.NBTViewerCache.AddNBTViewerCacheRow(tag.Name, tag.Count, "List");
        }

        private void AddCompoundTagHeader(CompoundTag tag)
        {
            this.cacheData.NBTViewerCache.AddNBTViewerCacheRow(tag.Name, null, "Compound");
        }

        private void AddEndTag()
        {
            this.cacheData.NBTViewerCache.AddNBTViewerCacheRow(null, null, "End");
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

        private void updateRowsRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpdateRows();
        }

        private void UpdateRows()
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                object obj = row.Cells[2].Value;
                if (obj != null)
                {
                    if (obj.ToString() == NBTTagType.COMPOUND.ToNameString())
                    {
                        this.dataGridView1.Rows[row.Index].Cells[0].ReadOnly = false;
                        this.dataGridView1.Rows[row.Index].Cells[0].Style = new DataGridViewCellStyle();
                        this.dataGridView1.Rows[row.Index].Cells[1].Value = null;
                        this.dataGridView1.Rows[row.Index].Cells[1].ReadOnly = true;
                        this.dataGridView1.Rows[row.Index].Cells[1].Style = this.ReadOnlyCellStyle;
                    }
                    else if (obj.ToString() == NBTTagType.LIST.ToNameString())
                    {
                        this.dataGridView1.Rows[row.Index].Cells[0].Value = null;
                        this.dataGridView1.Rows[row.Index].Cells[0].ReadOnly = true;
                        this.dataGridView1.Rows[row.Index].Cells[0].Style = this.ReadOnlyCellStyle;
                        this.dataGridView1.Rows[row.Index].Cells[1].ReadOnly = false;
                        this.dataGridView1.Rows[row.Index].Cells[1].Style = new DataGridViewCellStyle();
                    }
                    else if (obj.ToString() == NBTTagType.END.ToNameString())
                    {
                        this.dataGridView1.Rows[row.Index].Cells[0].Value = null;
                        this.dataGridView1.Rows[row.Index].Cells[0].ReadOnly = true;
                        this.dataGridView1.Rows[row.Index].Cells[0].Style = this.ReadOnlyCellStyle;
                        this.dataGridView1.Rows[row.Index].Cells[1].Value = null;
                        this.dataGridView1.Rows[row.Index].Cells[1].ReadOnly = true;
                        this.dataGridView1.Rows[row.Index].Cells[1].Style = this.ReadOnlyCellStyle;
                    }
                    else
                    {
                        this.dataGridView1.Rows[row.Index].Cells[0].ReadOnly = false;
                        this.dataGridView1.Rows[row.Index].Cells[0].Style = new DataGridViewCellStyle();
                        this.dataGridView1.Rows[row.Index].Cells[1].ReadOnly = false;
                        this.dataGridView1.Rows[row.Index].Cells[1].Style = new DataGridViewCellStyle();
                    }
                }
            }
        }

        public void MoveUpCell()
        {
            int index = this.dataGridView1.CurrentRow.Index;
            if (index != 0 && index != this.dataGridView1.Rows.Count - 1)
            {
                object[] c1 = this.cacheData.NBTViewerCache.Rows[index].ItemArray;
                object[] c2 = this.cacheData.NBTViewerCache.Rows[index - 1].ItemArray;
                this.cacheData.NBTViewerCache.Rows[index].ItemArray = c2;
                this.cacheData.NBTViewerCache.Rows[index - 1].ItemArray = c1;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index - 1].Cells[this.dataGridView1.CurrentCell.ColumnIndex];
                this.UpdateRows();
            }
        }

        public void MoveDownCell()
        {
            int index = this.dataGridView1.CurrentRow.Index;
            if (index + 1 < this.dataGridView1.Rows.Count - 1)
            {
                object[] c1 = this.cacheData.NBTViewerCache.Rows[index].ItemArray;
                object[] c2 = this.cacheData.NBTViewerCache.Rows[index + 1].ItemArray;
                this.cacheData.NBTViewerCache.Rows[index].ItemArray = c2;
                this.cacheData.NBTViewerCache.Rows[index + 1].ItemArray = c1;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index + 1].Cells[this.dataGridView1.CurrentCell.ColumnIndex];
                this.UpdateRows();
            }
        }

        private void littleEndianLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.littleEndianLToolStripMenuItem.Checked = true;
            this.bigEndianBToolStripMenuItem.Checked = false;
            this.Endian = NBTEndian.LITTLE_ENDIAN;
        }

        private void bigEndianBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.littleEndianLToolStripMenuItem.Checked = false;
            this.bigEndianBToolStripMenuItem.Checked = true;
            this.Endian = NBTEndian.BIG_ENDIAN;
        }
    }
}
