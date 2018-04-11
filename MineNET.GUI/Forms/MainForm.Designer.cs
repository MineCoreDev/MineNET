namespace MineNET.GUI.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nBTViewerNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mineNETGUIVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputOutput1 = new MineNET.GUI.Items.InputOutput();
            this.playerList1 = new MineNET.GUI.Items.PlayerList();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.controlCToolStripMenuItem,
            this.debugDToolStripMenuItem,
            this.toolTToolStripMenuItem,
            this.helpHToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitEToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // exitEToolStripMenuItem
            // 
            this.exitEToolStripMenuItem.Name = "exitEToolStripMenuItem";
            this.exitEToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.exitEToolStripMenuItem.Text = "Exit(&E)";
            this.exitEToolStripMenuItem.Click += new System.EventHandler(this.closeEToolStripMenuItem_Click);
            // 
            // controlCToolStripMenuItem
            // 
            this.controlCToolStripMenuItem.Name = "controlCToolStripMenuItem";
            this.controlCToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.controlCToolStripMenuItem.Text = "Control(&C)";
            // 
            // debugDToolStripMenuItem
            // 
            this.debugDToolStripMenuItem.Name = "debugDToolStripMenuItem";
            this.debugDToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.debugDToolStripMenuItem.Text = "Debug(&D)";
            // 
            // toolTToolStripMenuItem
            // 
            this.toolTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nBTViewerNToolStripMenuItem});
            this.toolTToolStripMenuItem.Name = "toolTToolStripMenuItem";
            this.toolTToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.toolTToolStripMenuItem.Text = "Tool(&T)";
            // 
            // nBTViewerNToolStripMenuItem
            // 
            this.nBTViewerNToolStripMenuItem.Name = "nBTViewerNToolStripMenuItem";
            this.nBTViewerNToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.nBTViewerNToolStripMenuItem.Text = "NBT Viewer(&N)";
            this.nBTViewerNToolStripMenuItem.Click += new System.EventHandler(this.nBTViewerNToolStripMenuItem_Click);
            // 
            // helpHToolStripMenuItem
            // 
            this.helpHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mineNETGUIVersionToolStripMenuItem});
            this.helpHToolStripMenuItem.Name = "helpHToolStripMenuItem";
            this.helpHToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.helpHToolStripMenuItem.Text = "Help(&H)";
            // 
            // mineNETGUIVersionToolStripMenuItem
            // 
            this.mineNETGUIVersionToolStripMenuItem.Name = "mineNETGUIVersionToolStripMenuItem";
            this.mineNETGUIVersionToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.mineNETGUIVersionToolStripMenuItem.Text = "MineNET GUI Version(&V)";
            this.mineNETGUIVersionToolStripMenuItem.Click += new System.EventHandler(this.mineNETGUIVersionToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(416, 323);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(497, 323);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 34);
            this.button2.TabIndex = 3;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(100, 26);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // inputOutput1
            // 
            this.inputOutput1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputOutput1.Location = new System.Drawing.Point(144, 27);
            this.inputOutput1.Name = "inputOutput1";
            this.inputOutput1.Size = new System.Drawing.Size(428, 277);
            this.inputOutput1.TabIndex = 4;
            // 
            // playerList1
            // 
            this.playerList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerList1.ContextMenuStrip = this.contextMenuStrip1;
            this.playerList1.Location = new System.Drawing.Point(12, 27);
            this.playerList1.Name = "playerList1";
            this.playerList1.Size = new System.Drawing.Size(126, 140);
            this.playerList1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.inputOutput1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.playerList1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MineNET GUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitEToolStripMenuItem;
        private Items.PlayerList playerList1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Items.InputOutput inputOutput1;
        private System.Windows.Forms.ToolStripMenuItem controlCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mineNETGUIVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nBTViewerNToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
    }
}