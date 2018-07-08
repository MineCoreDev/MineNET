using System;
using System.Windows.Forms;

namespace MineNET.GUI.UI.Forms
{
    public partial class LoadForm : Form
    {
        public bool AnyClose { get; private set; }
        public LoadForm()
        {
            InitializeComponent();
        }

        private void LoadForm_Shown(object sender, EventArgs e)
        {
            App.Instance.LoadLanguage();
            LanguageService.LanguageServiceInit();
            this.progressBar.Value += 10;
            this.ChangeStatus(LanguageService.GetString("app.loadForm.statusLabel.loadConfig"));
            App.Instance.LoadConfig();
            this.progressBar.Value += 10;
            this.ChangeStatus(LanguageService.GetString("app.loadForm.statusLabel.checkVersion"));

            MineNETGUIConfig config = App.Instance.Config;
            if (config.CheckVersion)
            {
                App.Instance.CheckVersion();
            }
            this.progressBar.Value = 100;
            this.AnyClose = true;
            this.Close();
        }

        public void ChangeStatus(string msg)
        {
            this.statusLabel.Text = msg;
        }

        private void LoadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.AnyClose)
            {
                e.Cancel = true;
            }
        }
    }
}
