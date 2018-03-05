using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                Queue<string> queue = Server.Instance.Logger.GuiLoggerTexts;
                if (queue.Count > 0)
                {
                    string text = queue.Dequeue();
                    if (!string.IsNullOrEmpty(text))
                    {
                        textBox1.Text += Server.Instance.Logger.GuiLoggerTexts.Dequeue();
                        textBox1.Text += Environment.NewLine;
                    }
                }

                await Task.Delay(1000 / 20);
            }
        }
    }
}
