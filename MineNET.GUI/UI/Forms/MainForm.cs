using MineNET.Events.IOEvents;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MineNET.GUI.UI.Forms
{
    public partial class MainForm : Form
    {
        public Server ServerInstance { get; private set; }
        public Thread ServerThread { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadForm f = new LoadForm();

            App.CreateInstance(this);

            this.Visible = false;
            f.ShowDialog();

            this.Activate();
        }

        private void timer_Tick(object sender, EventArgs e)
        {

        }

        private void openApplicationFolderAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenFolder(App.ExecutePath);
        }

        private void openPluginFolderPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenFolder(App.ExecutePath + "\\plugins");
        }

        private void openWorldFolderWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenFolder(App.ExecutePath + "\\worlds");
        }

        private void openReportFolderRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenFolder(App.ExecutePath + "\\reports");
        }

        private void OpenFolder(string path)
        {
            if (!App.Instance.OpenFileExproler(path))
            {
                MessageBox.Show(LanguageService.GetString("app.error.folderNotFound"),
                                LanguageService.GetString("app.error"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void exitEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(LanguageService.GetString("app.warning.closeApplication"),
                                                  LanguageService.GetString("app.warning"),
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ServerInstance == null)
            {
                this.ServerInstance = new Server();
                if (this.ServerInstance.Start())
                {
                    this.ServerThread = new Thread(this.ServerInstance.StartUpdate);
                    this.ServerThread.Start();

                    this.button1.Enabled = false;
                    this.button2.Enabled = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.ServerInstance != null)
            {
                if (this.ServerInstance.Stop())
                {
                    this.ServerInstance = null;
                    this.ServerThread = null;

                    this.button1.Enabled = true;
                    this.button2.Enabled = false;
                }
            }
        }
    }
}
