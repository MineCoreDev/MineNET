using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineNET.IO;
using System.Collections.Concurrent;

namespace MineNET.GUI.UI.Controls
{
    public partial class ConsoleControl : UserControl
    {
        public ConcurrentQueue<LoggerData> OutputQueue { get; private set; }
        public ConsoleControl()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.RefQueue(Server.Instance);

            if (this.OutputQueue != null)
            {
                LoggerData data;
                if (this.OutputQueue.TryDequeue(out data))
                {
                    this.WriteOutputLine(data.Text);
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {

        }

        public void RefQueue(Server server)
        {
            if (server != null)
            {
                if (server.Logger is Logger)
                {
                    Logger logger = (Logger)Server.Instance.Logger;
                    this.OutputQueue = logger.LoggerQueue;
                }
                else
                {
                    MessageBox.Show("Not Support Logger");
                }
            }
        }

        public void WriteOutputLine(string text)
        {
            this.richTextBox.AppendText(text + Environment.NewLine);
        }
    }
}
