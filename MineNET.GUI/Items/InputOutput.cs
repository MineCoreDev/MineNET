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
        }

        internal async void OnUpdate()
        {
            while (!Server.Instance.IsShutdown())
            {
                Queue<LoggerInfo> queue = Server.Instance.Logger.GuiLoggerTexts;
                if (queue.Count == 0)
                {
                    await Task.Delay(1000 / 200);
                    continue;
                }
                else
                {
                    LoggerInfo info = queue.Dequeue();
                    if (this.CheckShowOutput(info.Level))
                    {
                        if (!string.IsNullOrEmpty(info.Text))
                        {
                            textBox1.AppendText(info.Text + Environment.NewLine);
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

        internal string InputButtonLabel
        {
            get
            {
                return this.button1.Text;
            }

            set
            {
                this.button1.Text = value;
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

        internal string OutputClearButtonLabel
        {
            get
            {
                return this.button2.Text;
            }

            set
            {
                this.button2.Text = value;
            }
        }

        internal CheckedListBox OutputOptionCheckBox
        {
            get
            {
                return checkedListBox1;
            }
        }

        private bool CheckShowOutput(LoggerLevel level)
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
