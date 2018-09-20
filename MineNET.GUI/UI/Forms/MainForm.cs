using MineNET.Events.ServerEvents;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MineNET.GUI.UI.Forms
{
    public partial class MainForm : Form
    {
        public Server ServerInstance { get; private set; }
        public Thread ServerThread { get; private set; }

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
            if (this.ServerInstance == null)
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
            else
            {
                DialogResult result = MessageBox.Show(LanguageService.GetString("app.warning.closeServerAndApplication"),
                                                  LanguageService.GetString("app.warning"),
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
                else
                {
                    this.ServerInstance.Stop();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ServerInstance == null)
            {
                this.ServerInstance = new Server();
                this.consoleControl.LoggerSettings();
                this.ServerThread = new Thread(() =>
                {
                    if (this.ServerInstance.Start())
                    {
                        this.ServerInstance.Event.Server.ServerStop += Server_ServerStop;

                        this.button1.Enabled = false;
                        this.button2.Enabled = true;

                        this.ServerInstance.StartUpdate();
                    }
                });
                this.ServerThread.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.ServerInstance != null)
            {
                this.ServerInstance.Invoke(() => this.ServerInstance.Stop());

            }
        }

        private void Server_ServerStop(object sender, ServerStopEventArgs e)
        {
            this.ServerInstance = null;
            this.ServerThread = null;

            this.button1.Enabled = true;
            this.button2.Enabled = false;
        }
    }
}
