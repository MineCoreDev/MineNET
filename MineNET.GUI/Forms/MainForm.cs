using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineNET.GUI.Forms
{
    public partial class MainForm : Form
    {
        public LoadForm BaseForm { get; }
        public Server Server { get; set; }

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

        private async void ServerStart()
        {
            Server = new Server();
            Server.Start(true);
            Server.Logger.UseGUI = true;
            inputOutput1.OnUpdate();
            await Task.Delay(100);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.BaseForm.Close();
        }

        private void closeEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Server != null)
            {
                return;
            }

            this.button1.Enabled = false;

            ServerStart();
        }
    }
}
