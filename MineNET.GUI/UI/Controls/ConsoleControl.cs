using System.Windows.Forms;

namespace MineNET.GUI.UI.Controls
{
    public partial class ConsoleControl : UserControl
    {
        /*public ConcurrentQueue<LoggerData> OutputQueue { get; private set; }
        public ConsoleControl()
        {
            InitializeComponent();

            this.comboBox.Items.Add(LanguageService.GetString("app.consoleControl.comboBoxItem.consoleMode"));
            this.comboBox.Items.Add(LanguageService.GetString("app.consoleControl.comboBoxItem.sayMode"));
            this.comboBox.SelectedIndex = 0;

            this.label1.Text = LanguageService.GetString("app.consoleControl.label1");
            this.label2.Text = LanguageService.GetString("app.consoleControl.label2");
            this.label3.Text = LanguageService.GetString("app.consoleControl.label3");

            this.button.Text = LanguageService.GetString("app.consoleControl.button");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.AttachQueue(Server.Instance);
            if (this.OutputQueue != null)
            {
                LoggerData data;
                if (this.OutputQueue.TryDequeue(out data))
                {
                    this.WriteOutputLine(data.Text);
                }
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter && textBox.Focused)
            {
                this.SendCommand();
                e.Handled = true;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.SendCommand();
        }

        public void AttachQueue(Server server)
        {
            if (server != null)
            {
                this.OutputQueue = server.Logger.Output.OutputQueue;
            }
        }

        public void WriteOutputLine(string text)
        {
            this.richTextBox.AppendText(text + Environment.NewLine);
        }

        public void ConsoleClear()
        {
            this.richTextBox.Clear();
        }

        public void SendCommand()
        {
            string text = this.textBox.Text;
            if (Server.Instance != null && !string.IsNullOrEmpty(text))
            {
                Server.Instance.Logger.Input.AddInputQueue(text);
                this.textBox.Clear();
            }
        }*/
    }
}
