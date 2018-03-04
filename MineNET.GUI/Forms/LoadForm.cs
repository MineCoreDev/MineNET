using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineNET.GUI.Forms
{
    public partial class LoadForm : Form
    {
        public LoadForm()
        {
            InitializeComponent();
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            this.LoadProgress();
        }

        private async void LoadProgress()
        {
            this.statusAndProgress1.Text = "SetupLanguage...";
            //TODO: Language Setup...
            await Task.Delay(500);
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

        private void Loaded()
        {

        }
    }
}
