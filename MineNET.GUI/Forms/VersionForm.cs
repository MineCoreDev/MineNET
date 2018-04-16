using System.Windows.Forms;

namespace MineNET.GUI.Forms
{
    public partial class VersionForm : Form
    {
        public VersionForm()
        {
            this.InitializeComponent();
            this.SetupLanguage();
        }

        private void SetupLanguage()
        {
            this.Text = LangManager.GetString("versionForm_text");
        }
    }
}
