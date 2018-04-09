namespace MineNET.GUI.Forms
{
    partial class NBTViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.keyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagTypeDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.nBTViewerCacheBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cacheData = new MineNET.GUI.Resources.Data.CacheData();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updateRowsRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nBTViewerCacheBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cacheData)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.keyDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn,
            this.tagTypeDataGridViewComboBoxColumn});
            this.dataGridView1.DataSource = this.nBTViewerCacheBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(484, 237);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dataGridView1_CellParsing);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            // 
            // keyDataGridViewTextBoxColumn
            // 
            this.keyDataGridViewTextBoxColumn.DataPropertyName = "Key";
            this.keyDataGridViewTextBoxColumn.HeaderText = "Key";
            this.keyDataGridViewTextBoxColumn.Name = "keyDataGridViewTextBoxColumn";
            this.keyDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tagTypeDataGridViewComboBoxColumn
            // 
            this.tagTypeDataGridViewComboBoxColumn.DataPropertyName = "TagType";
            this.tagTypeDataGridViewComboBoxColumn.HeaderText = "TagType";
            this.tagTypeDataGridViewComboBoxColumn.Items.AddRange(new object[] {
            "End",
            "Byte",
            "Short",
            "Int",
            "Long",
            "Float",
            "Double",
            "ByteArray",
            "String",
            "List",
            "Compound",
            "IntArray",
            "LongArray"});
            this.tagTypeDataGridViewComboBoxColumn.Name = "tagTypeDataGridViewComboBoxColumn";
            this.tagTypeDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // nBTViewerCacheBindingSource
            // 
            this.nBTViewerCacheBindingSource.AllowNew = true;
            this.nBTViewerCacheBindingSource.DataMember = "NBTViewerCache";
            this.nBTViewerCacheBindingSource.DataSource = this.cacheData;
            // 
            // cacheData
            // 
            this.cacheData.DataSetName = "CacheData";
            this.cacheData.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.actionAToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(484, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // actionAToolStripMenuItem
            // 
            this.actionAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveUpUToolStripMenuItem,
            this.moveDownDToolStripMenuItem,
            this.updateRowsRToolStripMenuItem});
            this.actionAToolStripMenuItem.Name = "actionAToolStripMenuItem";
            this.actionAToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.actionAToolStripMenuItem.Text = "Action(&A)";
            // 
            // moveUpUToolStripMenuItem
            // 
            this.moveUpUToolStripMenuItem.Name = "moveUpUToolStripMenuItem";
            this.moveUpUToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.moveUpUToolStripMenuItem.Text = "Move Up(&U)";
            this.moveUpUToolStripMenuItem.Click += new System.EventHandler(this.moveUpUToolStripMenuItem_Click);
            // 
            // moveDownDToolStripMenuItem
            // 
            this.moveDownDToolStripMenuItem.Name = "moveDownDToolStripMenuItem";
            this.moveDownDToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.moveDownDToolStripMenuItem.Text = "Move Down(&D)";
            this.moveDownDToolStripMenuItem.Click += new System.EventHandler(this.moveDownDToolStripMenuItem_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Value";
            this.dataGridViewTextBoxColumn1.HeaderText = "Value";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // updateRowsRToolStripMenuItem
            // 
            this.updateRowsRToolStripMenuItem.Name = "updateRowsRToolStripMenuItem";
            this.updateRowsRToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.updateRowsRToolStripMenuItem.Text = "Update Rows(&R)";
            this.updateRowsRToolStripMenuItem.Click += new System.EventHandler(this.updateRowsRToolStripMenuItem_Click);
            // 
            // NBTViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "NBTViewer";
            this.Text = "NBTViewer";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nBTViewerCacheBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cacheData)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveUpUToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownDToolStripMenuItem;
        private System.Windows.Forms.BindingSource nBTViewerCacheBindingSource;
        private Resources.Data.CacheData cacheData;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn tagTypeDataGridViewComboBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem updateRowsRToolStripMenuItem;
    }
}