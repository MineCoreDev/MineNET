using System;
using System.Windows.Forms;

namespace MineNET.GUI.UI.Modals
{
    public partial class ConfigSetupModal : Form
    {
        public bool CheckBoxResult1 { get; private set; }
        public bool CheckBoxResult2 { get; private set; }

        private bool CloseFlag { get; set; }

        public ConfigSetupModal()
        {
            InitializeComponent();

            this.checkBox1.Text = LanguageService.GetString("app.config.checkVersion");
            this.checkBox2.Text = LanguageService.GetString("app.config.showNews");
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.CheckBoxResult1 = this.checkBox1.Checked;
            this.CheckBoxResult2 = this.checkBox2.Checked;

            this.CloseFlag = true;
            this.DialogResult = DialogResult.OK;
        }

        private void ConfigSetupModal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.CloseFlag)
                e.Cancel = true;
        }
    }
}
