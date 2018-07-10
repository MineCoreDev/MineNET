using MineNET.GUI.Data;
using System.Windows.Forms;

namespace MineNET.GUI.UI.Modals
{
    public partial class LanguageSelectModal : Form
    {
        public Language SelectResult { get; private set; }

        private bool CloseFlag { get; set; }

        public LanguageSelectModal()
        {
            InitializeComponent();
        }

        private void LanguageSelectModal_Load(object sender, System.EventArgs e)
        {
            Language[] support = App.SupportLanguage;
            foreach (Language item in support)
            {
                this.comboBox.Items.Add(item.ToString());
            }
            this.comboBox.SelectedIndex = 0;
        }

        private void LanguageSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.CloseFlag)
                e.Cancel = true;
        }

        private void button_Click(object sender, System.EventArgs e)
        {
            int index = this.comboBox.SelectedIndex;
            if (index != -1)
            {
                Language[] support = App.SupportLanguage;
                this.SelectResult = support[index];
                this.CloseFlag = true;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
