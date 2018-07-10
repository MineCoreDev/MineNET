using System.Windows.Forms;

namespace MineNET.GUI.UI.Controls
{
    public partial class PlayerListControl : UserControl
    {
        public PlayerListControl()
        {
            InitializeComponent();
        }

        private void PlayerListControl_Load(object sender, System.EventArgs e)
        {
            this.label.Text = LanguageService.GetString("app.playerListControl.label");
        }

        public void UpdatePlayerList()
        {

        }
    }
}
