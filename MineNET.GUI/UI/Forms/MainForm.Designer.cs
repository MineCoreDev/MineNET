namespace MineNET.GUI.UI.Forms
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
            this.openOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openApplicationFolderAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPluginFolderPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWorldFolderWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openReportFolderRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.restartRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kickKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.messageMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teleportTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.editNBTNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.worldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nBTViewerNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerToolsPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mineNETVersionInfomationVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.optionOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.playerListControl1 = new MineNET.GUI.UI.Controls.PlayerListControl();
            this.menuStrip1.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openOToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitEToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // openOToolStripMenuItem
            // 
            this.openOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openApplicationFolderAToolStripMenuItem,
            this.openPluginFolderPToolStripMenuItem,
            this.openWorldFolderWToolStripMenuItem,
            this.openReportFolderRToolStripMenuItem});
            this.openOToolStripMenuItem.Name = "openOToolStripMenuItem";
            this.openOToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.openOToolStripMenuItem.Text = "Open(&O)";
            // 
            // openApplicationFolderAToolStripMenuItem
            // 
            this.openApplicationFolderAToolStripMenuItem.Name = "openApplicationFolderAToolStripMenuItem";
            this.openApplicationFolderAToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.openApplicationFolderAToolStripMenuItem.Text = "Open Application Folder(&A)";
            this.openApplicationFolderAToolStripMenuItem.Click += new System.EventHandler(this.openApplicationFolderAToolStripMenuItem_Click);
            // 
            // openPluginFolderPToolStripMenuItem
            // 
            this.openPluginFolderPToolStripMenuItem.Name = "openPluginFolderPToolStripMenuItem";
            this.openPluginFolderPToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.openPluginFolderPToolStripMenuItem.Text = "Open Plugin Folder(&P)";
            this.openPluginFolderPToolStripMenuItem.Click += new System.EventHandler(this.openPluginFolderPToolStripMenuItem_Click);
            // 
            // openWorldFolderWToolStripMenuItem
            // 
            this.openWorldFolderWToolStripMenuItem.Name = "openWorldFolderWToolStripMenuItem";
            this.openWorldFolderWToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.openWorldFolderWToolStripMenuItem.Text = "Open World Folder(&W)";
            this.openWorldFolderWToolStripMenuItem.Click += new System.EventHandler(this.openWorldFolderWToolStripMenuItem_Click);
            // 
            // openReportFolderRToolStripMenuItem
            // 
            this.openReportFolderRToolStripMenuItem.Name = "openReportFolderRToolStripMenuItem";
            this.openReportFolderRToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.openReportFolderRToolStripMenuItem.Text = "Open Report Folder(&R)";
            this.openReportFolderRToolStripMenuItem.Click += new System.EventHandler(this.openReportFolderRToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(117, 6);
            // 
            // exitEToolStripMenuItem
            // 
            this.exitEToolStripMenuItem.Name = "exitEToolStripMenuItem";
            this.exitEToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.exitEToolStripMenuItem.Text = "Exit(&E)";
            this.exitEToolStripMenuItem.Click += new System.EventHandler(this.exitEToolStripMenuItem_Click);
            // 
            // controlCToolStripMenuItem
            // 
            this.controlCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverSToolStripMenuItem,
            this.playerPToolStripMenuItem,
            this.worldToolStripMenuItem,
            this.networkNToolStripMenuItem,
            this.pluginToolStripMenuItem});
            this.controlCToolStripMenuItem.Name = "controlCToolStripMenuItem";
            this.controlCToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.controlCToolStripMenuItem.Text = "Control(&C)";
            // 
            // serverSToolStripMenuItem
            // 
            this.serverSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startSToolStripMenuItem,
            this.stopTToolStripMenuItem,
            this.toolStripMenuItem2,
            this.restartRToolStripMenuItem});
            this.serverSToolStripMenuItem.Name = "serverSToolStripMenuItem";
            this.serverSToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.serverSToolStripMenuItem.Text = "Server(&S)";
            // 
            // startSToolStripMenuItem
            // 
            this.startSToolStripMenuItem.Name = "startSToolStripMenuItem";
            this.startSToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.startSToolStripMenuItem.Text = "Start(&S)";
            // 
            // stopTToolStripMenuItem
            // 
            this.stopTToolStripMenuItem.Name = "stopTToolStripMenuItem";
            this.stopTToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.stopTToolStripMenuItem.Text = "Stop(&T)";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(122, 6);
            // 
            // restartRToolStripMenuItem
            // 
            this.restartRToolStripMenuItem.Name = "restartRToolStripMenuItem";
            this.restartRToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.restartRToolStripMenuItem.Text = "Restart(&R)";
            // 
            // playerPToolStripMenuItem
            // 
            this.playerPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kickKToolStripMenuItem,
            this.toolStripMenuItem3,
            this.messageMToolStripMenuItem,
            this.teleportTToolStripMenuItem,
            this.killLToolStripMenuItem,
            this.itemIToolStripMenuItem,
            this.toolStripMenuItem4,
            this.editNBTNToolStripMenuItem});
            this.playerPToolStripMenuItem.Name = "playerPToolStripMenuItem";
            this.playerPToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.playerPToolStripMenuItem.Text = "Player(&P)";
            // 
            // kickKToolStripMenuItem
            // 
            this.kickKToolStripMenuItem.Name = "kickKToolStripMenuItem";
            this.kickKToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.kickKToolStripMenuItem.Text = "Kick(&K)";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(136, 6);
            // 
            // messageMToolStripMenuItem
            // 
            this.messageMToolStripMenuItem.Name = "messageMToolStripMenuItem";
            this.messageMToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.messageMToolStripMenuItem.Text = "Message(&M)";
            // 
            // teleportTToolStripMenuItem
            // 
            this.teleportTToolStripMenuItem.Name = "teleportTToolStripMenuItem";
            this.teleportTToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.teleportTToolStripMenuItem.Text = "Teleport(&T)";
            // 
            // killLToolStripMenuItem
            // 
            this.killLToolStripMenuItem.Name = "killLToolStripMenuItem";
            this.killLToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.killLToolStripMenuItem.Text = "Kill(&L)";
            // 
            // itemIToolStripMenuItem
            // 
            this.itemIToolStripMenuItem.Name = "itemIToolStripMenuItem";
            this.itemIToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.itemIToolStripMenuItem.Text = "Item(&I)";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(136, 6);
            // 
            // editNBTNToolStripMenuItem
            // 
            this.editNBTNToolStripMenuItem.Name = "editNBTNToolStripMenuItem";
            this.editNBTNToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.editNBTNToolStripMenuItem.Text = "Edit NBT(&N)";
            // 
            // worldToolStripMenuItem
            // 
            this.worldToolStripMenuItem.Name = "worldToolStripMenuItem";
            this.worldToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.worldToolStripMenuItem.Text = "World";
            // 
            // networkNToolStripMenuItem
            // 
            this.networkNToolStripMenuItem.Name = "networkNToolStripMenuItem";
            this.networkNToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.networkNToolStripMenuItem.Text = "Network(&N)";
            // 
            // pluginToolStripMenuItem
            // 
            this.pluginToolStripMenuItem.Name = "pluginToolStripMenuItem";
            this.pluginToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.pluginToolStripMenuItem.Text = "Plugin";
            // 
            // debugDToolStripMenuItem
            // 
            this.debugDToolStripMenuItem.Name = "debugDToolStripMenuItem";
            this.debugDToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.debugDToolStripMenuItem.Text = "Debug(&D)";
            // 
            // toolTToolStripMenuItem
            // 
            this.toolTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nBTViewerNToolStripMenuItem,
            this.playerToolsPToolStripMenuItem,
            this.toolStripMenuItem5,
            this.optionOToolStripMenuItem});
            this.toolTToolStripMenuItem.Name = "toolTToolStripMenuItem";
            this.toolTToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.toolTToolStripMenuItem.Text = "Tool(&T)";
            // 
            // nBTViewerNToolStripMenuItem
            // 
            this.nBTViewerNToolStripMenuItem.Name = "nBTViewerNToolStripMenuItem";
            this.nBTViewerNToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.nBTViewerNToolStripMenuItem.Text = "NBT Viewer(&N)";
            // 
            // playerToolsPToolStripMenuItem
            // 
            this.playerToolsPToolStripMenuItem.Name = "playerToolsPToolStripMenuItem";
            this.playerToolsPToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.playerToolsPToolStripMenuItem.Text = "Player Tools(&P)";
            // 
            // helpHToolStripMenuItem
            // 
            this.helpHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mineNETVersionInfomationVToolStripMenuItem});
            this.helpHToolStripMenuItem.Name = "helpHToolStripMenuItem";
            this.helpHToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.helpHToolStripMenuItem.Text = "Help(&H)";
            // 
            // mineNETVersionInfomationVToolStripMenuItem
            // 
            this.mineNETVersionInfomationVToolStripMenuItem.Name = "mineNETVersionInfomationVToolStripMenuItem";
            this.mineNETVersionInfomationVToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.mineNETVersionInfomationVToolStripMenuItem.Text = "MineNET Version Infomation(&V)";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(148, 6);
            // 
            // optionOToolStripMenuItem
            // 
            this.optionOToolStripMenuItem.Name = "optionOToolStripMenuItem";
            this.optionOToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.optionOToolStripMenuItem.Text = "Option(&O)...";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(606, 504);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 45);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(692, 504);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 45);
            this.button2.TabIndex = 3;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // playerListControl1
            // 
            this.playerListControl1.Location = new System.Drawing.Point(12, 27);
            this.playerListControl1.Name = "playerListControl1";
            this.playerListControl1.Size = new System.Drawing.Size(150, 522);
            this.playerListControl1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.playerListControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MineNET-GUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openApplicationFolderAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPluginFolderPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWorldFolderWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openReportFolderRToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpHToolStripMenuItem;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem serverSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopTToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem restartRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kickKToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem messageMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem teleportTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem killLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemIToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem editNBTNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem worldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nBTViewerNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerToolsPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mineNETVersionInfomationVToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem optionOToolStripMenuItem;
        private Controls.PlayerListControl playerListControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}