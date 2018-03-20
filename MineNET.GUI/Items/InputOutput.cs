using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineNET.Utils;

namespace MineNET.GUI.Items
{
    public partial class InputOutput : UserControl
    {
        public InputOutput()
        {
            InitializeComponent();

            checkedListBox1.SetItemChecked(0, false);
            checkedListBox1.SetItemChecked(1, true);
            checkedListBox1.SetItemChecked(2, true);
            checkedListBox1.SetItemChecked(3, true);
            checkedListBox1.SetItemChecked(4, true);
            checkedListBox1.SetItemChecked(5, true);
        }

        internal async void OnUpdate()
        {
            while (!Server.Instance.IsShutdown())
            {
                Queue<Logger.LoggerInfo> queue = Server.Instance.Logger.GuiLoggerTexts;
                if (queue.Count == 0)
                {
                    await Task.Delay(1000 / 200);
                    continue;
                }
                else
                {
                    Logger.LoggerInfo info = queue.Dequeue();
                    if (this.CheckShowOutput(info.level))
                    {
                        if (!string.IsNullOrEmpty(info.text))
                        {
                            textBox1.AppendText(info.text + Environment.NewLine);
                        }
                    }
                }

                await Task.Delay(1000 / 200);
            }
        }

        internal void SendCommand()
        {
            string cmd = textBox2.Text;
            if (!string.IsNullOrWhiteSpace(cmd) && Server.Instance != null)
            {
                Server.Instance.CommandManager.HandleConsoleCommand(textBox2.Text);
            }
            textBox2.Clear();
            textBox2.Focus();
        }

        internal TextBox Input
        {
            get
            {
                return this.textBox2;
            }
        }

        internal string InputLabel
        {
            get
            {
                return this.label1.Text;
            }

            set
            {
                this.label1.Text = value;
            }
        }

        internal string OutputLabel
        {
            get
            {
                return this.label2.Text;
            }

            set
            {
                this.label2.Text = value;
            }
        }

        internal string OutputOptionLabel
        {
            get
            {
                return this.label3.Text;
            }

            set
            {
                this.label3.Text = value;
            }
        }

        private bool CheckShowOutput(Logger.LoggerLevel level)
        {
            int t = (int) level;
            return checkedListBox1.GetItemChecked(t);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendCommand();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter && textBox2.Focused)
            {
                SendCommand();
                e.Handled = true;
            }
        }
    }
}
