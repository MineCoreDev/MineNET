using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineNET.Utils.Config;

namespace MineNET.GUI.Forms
{
    public partial class LoadForm : Form
    {
        public LoadForm()
        {
            this.InitializeComponent();
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            this.LoadProgress();
        }

        private async void LoadProgress()
        {
            this.statusAndProgress1.Text = "SetupLanguage...";

            this.LoadConfigs();

            this.statusAndProgress1.Text = LangManager.GetString("start_lang_setup");
            this.statusAndProgress1.ProgressBar.Value = 25;

            await Task.Delay(200);

            this.statusAndProgress1.Text = LangManager.GetString("start_load");
            this.statusAndProgress1.ProgressBar.Value = 50;

            await Task.Delay(200);

            this.statusAndProgress1.Text = LangManager.GetString("start_loaded");
            this.statusAndProgress1.ProgressBar.Value = 100;

            this.Loaded();
        }

        private void LoadConfigs()
        {
            string mPath = $"{Server.ExecutePath}\\MineNET.yml";
            MineNETConfig conf = YamlStaticConfig.Load<MineNETConfig>(mPath);
            LangManager.Language = conf.Language;
            MineNET.Utils.LangManager.Lang = conf.Language;
        }

        private void Loaded()
        {
            this.Visible = false;

            MainForm form = new MainForm(this);
            form.ShowDialog();
        }
    }
}
