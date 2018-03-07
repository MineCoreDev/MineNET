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

        private bool CheckShowOutput(Logger.LoggerLevel level)
        {
            int t = (int) level;
            return checkedListBox1.GetItemChecked(t);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
