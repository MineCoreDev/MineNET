using System;
using System.Windows.Forms;

namespace MineNET.GUI.UI.Forms
{
    public partial class MainForm : Form
    {
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
    }
}
