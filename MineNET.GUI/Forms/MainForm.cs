using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineNET.Events.ServerEvents;
using MineNET.GUI.Config;
using MineNET.NBT.Tags;
using MineNET.Utils.Config;

namespace MineNET.GUI.Forms
{
    public partial class MainForm : Form
    {
        public LoadForm BaseForm { get; }
        public Server Server { get; set; }

        public static MineNETGUIConfig Config { get; private set; }

        public MainForm(LoadForm form)
        {
            this.InitializeComponent();
            this.button2.Enabled = false;
            this.SetupLanguage();
            this.BaseForm = form;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.ConfigLoad();
        }

        private void SetupLanguage()
        {
            this.button1.Text = LangManager.GetString("form_button1_label");
            this.button2.Text = LangManager.GetString("form_button2_label");

            this.playerList1.Text = LangManager.GetString("form_playerList_label");
            this.inputOutput1.InputLabel = LangManager.GetString("form_input_label");
            this.inputOutput1.InputButtonLabel = LangManager.GetString("form_inputButton_label");
            this.inputOutput1.OutputLabel = LangManager.GetString("form_output_label");
            this.inputOutput1.OutputOptionLabel = LangManager.GetString("form_outputOption_label");
            this.inputOutput1.OutputClearButtonLabel = LangManager.GetString("form_outputClearButton_label");
            this.inputOutput1.InputModeLabel = LangManager.GetString("form_inputMode_label");

            this.fileFToolStripMenuItem.Text = LangManager.GetString("form_fileFToolStripMenuItem_label");
            this.exitEToolStripMenuItem.Text = LangManager.GetString("form_exitEToolStripMenuItem_label");

            this.controlCToolStripMenuItem.Text = LangManager.GetString("form_controlCToolStripMenuItem_label");

            this.debugDToolStripMenuItem.Text = LangManager.GetString("form_debugDToolStripMenuItem_label");

            this.toolTToolStripMenuItem.Text = LangManager.GetString("form_toolTToolStripMenuItem_label");

            this.helpHToolStripMenuItem.Text = LangManager.GetString("form_helpHToolStripMenuItem_label");
            this.mineNETGUIVersionToolStripMenuItem.Text = LangManager.GetString("form_mineNETGUIVersionToolStripMenuItem_label");
        }

        private void ConfigLoad()
        {
            MainForm.Config = YamlStaticConfig.Load<MineNETGUIConfig>($"{Environment.CurrentDirectory}\\MineNET_GUI.yml");

            this.inputOutput1.OutputOptionCheckBox.SetItemChecked(0, MainForm.Config.OutputOption.Log);
            this.inputOutput1.OutputOptionCheckBox.SetItemChecked(1, MainForm.Config.OutputOption.Info);
            this.inputOutput1.OutputOptionCheckBox.SetItemChecked(2, MainForm.Config.OutputOption.Notice);
            this.inputOutput1.OutputOptionCheckBox.SetItemChecked(3, MainForm.Config.OutputOption.Warning);
            this.inputOutput1.OutputOptionCheckBox.SetItemChecked(4, MainForm.Config.OutputOption.Error);
            this.inputOutput1.OutputOptionCheckBox.SetItemChecked(5, MainForm.Config.OutputOption.Fatal);
            this.inputOutput1.InputModeComboBox.SelectedIndex = MainForm.Config.InputModeIndex;
        }

        private void ConfigSave()
        {
            Config.OutputOption.Log = this.inputOutput1.OutputOptionCheckBox.GetItemChecked(0);
            Config.OutputOption.Info = this.inputOutput1.OutputOptionCheckBox.GetItemChecked(1);
            Config.OutputOption.Notice = this.inputOutput1.OutputOptionCheckBox.GetItemChecked(2);
            Config.OutputOption.Warning = this.inputOutput1.OutputOptionCheckBox.GetItemChecked(3);
            Config.OutputOption.Error = this.inputOutput1.OutputOptionCheckBox.GetItemChecked(4);
            Config.OutputOption.Fatal = this.inputOutput1.OutputOptionCheckBox.GetItemChecked(5);
            Config.InputModeIndex = this.inputOutput1.InputModeComboBox.SelectedIndex;

            Config.Save<MineNETGUIConfig>();
        }

        private async void ServerStart()
        {
            try
            {
                this.button1.Enabled = false;
                this.button2.Enabled = true;

                ServerEvents.ServerStop += ServerEvents_ServerStop;

                this.Server = new Server();
                this.Server.Start();
                this.inputOutput1.OnUpdate();
                this.playerList1.OnUpdate();
                await Task.Delay(100);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), LangManager.GetString("app_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Server.ErrorStop(e);
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
            DialogResult result = MessageBox.Show(LangManager.GetString("app_exit_alert"), LangManager.GetString("app_exit"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ConfigSave();
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

            this.Server.Stop();

            this.ServerStop();
        }

        private void mineNETGUIVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new VersionForm();
            f.ShowDialog();
        }

        private void nBTViewerNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompoundTag subTag = new CompoundTag();
            subTag.PutBool("bool", true);
            subTag.PutByte("byte", 123);
            CompoundTag tag = new CompoundTag();
            tag.PutBool("bool", true);
            tag.PutByte("byte", 123);
            //tag.PutByteArray("byteArray", ArrayUtils.CreateArray<byte>(200));
            tag.PutShort("short", 12345);
            tag.PutInt("int", 12345678);
            //tag.PutIntArray("intArray", ArrayUtils.CreateArray<int>(200));
            tag.PutLong("long", 123456789123456);
            //tag.PutLongArray("longArray", ArrayUtils.CreateArray<long>(200));
            tag.PutFloat("float", 12.3456f);
            tag.PutDouble("double", 12.3456789);
            //tag.PutList(list);
            tag.PutCompound("com", subTag);
            Form f = new NBTViewer(tag);
            f.ShowDialog();
        }
    }
}
