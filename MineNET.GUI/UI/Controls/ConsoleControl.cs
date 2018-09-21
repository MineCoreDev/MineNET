using NLog;
using NLog.Config;
using NLog.Windows.Forms;
using System;
using System.Windows.Forms;

namespace MineNET.GUI.UI.Controls
{
    public partial class ConsoleControl : UserControl
    {
        public ConsoleControl()
        {
            InitializeComponent();

            this.comboBox.Items.Add(LanguageService.GetString("app.consoleControl.comboBoxItem.consoleMode"));
            this.comboBox.Items.Add(LanguageService.GetString("app.consoleControl.comboBoxItem.sayMode"));
            this.comboBox.SelectedIndex = 0;

            this.label1.Text = LanguageService.GetString("app.consoleControl.label1");
            this.label2.Text = LanguageService.GetString("app.consoleControl.label2");
            this.label3.Text = LanguageService.GetString("app.consoleControl.label3");

            this.button1.Text = LanguageService.GetString("app.consoleControl.button1");
            this.button2.Text = LanguageService.GetString("app.consoleControl.button2");
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.ConsoleClear();
        }

        internal void LoggerSettings()
        {
            LoggingConfiguration conf = LogManager.Configuration;
            RichTextBoxTarget target = new RichTextBoxTarget();
            String back = "Black";
            target.TargetRichTextBox = this.richTextBox;
            target.AutoScroll = true;
            target.Layout = "[${longdate}] [${threadname} /${uppercase:${level:padding=5}}] ${message}";
            target.UseDefaultRowColoringRules = false;
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Debug", "DarkGray", back));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Trace", "Cyan", back));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Info", "Gray", back));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Warn", "Yellow", back));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Error", "Red", back));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Fatal", "White", "Red"));
            conf.AddTarget("richTextBox", target);
            conf.AddRuleForAllLevels(target);

            SimpleConfigurator.ConfigureForTargetLogging(target);
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
                Server.Instance.Logger.InputLogger.AddInputQueue(text);
                this.textBox.Clear();
            }
        }
    }
}
