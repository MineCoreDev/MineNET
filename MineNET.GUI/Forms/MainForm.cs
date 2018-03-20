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
            this.playerList1.Text = LangManager.GetString("form_playerList_label");
            this.inputOutput1.InputLabel = LangManager.GetString("form_input_label");
            this.inputOutput1.OutputLabel = LangManager.GetString("form_output_label");
            this.inputOutput1.OutputOptionLabel = LangManager.GetString("form_outputOption_label");
        }

        private async void ServerStart()
        {
            try
            {
                this.button1.Enabled = false;
                this.button2.Enabled = true;

                ServerEvents.ServerStop += ServerEvents_ServerStop;

                Server = new Server();
                Server.Start();
                inputOutput1.OnUpdate();
                playerList1.OnUpdate();
                await Task.Delay(100);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Server.ErrorStop(e);
                throw e;
            }
        }

        private void ServerStop()
        {
            this.button1.Enabled = true;
            this.button2.Enabled = false;

            this.Server = null;
        }

        private void ServerEvents_ServerStop(ServerStopEventArgs args)
        {
            ServerEvents.ServerStop -= ServerEvents_ServerStop;

            this.ServerStop();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Exit Application?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
            }
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
            if (this.Server != null)
            {
                return;
            }

            this.ServerStart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.Server == null)
            {
                return;
            }

            Server.Stop();

            this.ServerStop();
        }
    }
}
