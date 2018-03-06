using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineNET.Events.ServerEvents;

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
            this.button2.Enabled = false;
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
            ServerEvents.ServerStop += ServerEvents_ServerStop;

            Server = new Server();
            Server.Start();
            Server.Logger.UseGUI = true;
            inputOutput1.OnUpdate();
            await Task.Delay(100);
        }

        private void ServerEvents_ServerStop(ServerStopEventArgs args)
        {
            this.button1.Enabled = true;
            this.button2.Enabled = false;
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
            this.button2.Enabled = true;

            ServerStart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Server == null)
            {
                return;
            }

            Server.Stop();

            this.button1.Enabled = true;
            this.button2.Enabled = false;

            Server = null;
        }
    }
}
