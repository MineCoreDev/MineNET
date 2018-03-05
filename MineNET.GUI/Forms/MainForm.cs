using System;
using System.Windows.Forms;

namespace MineNET.GUI.Forms
{
    public partial class MainForm : Form
    {
        public LoadForm BaseForm { get; }

        public MainForm(LoadForm form)
        {
            this.InitializeComponent();
            this.BaseForm = form;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupLanguage();
        }

        private void SetupLanguage()
        {
            
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.BaseForm.Close();
        }
    }
}
