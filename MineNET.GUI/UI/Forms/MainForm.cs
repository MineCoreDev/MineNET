using System;
using System.Windows.Forms;

namespace MineNET.GUI.UI.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadForm f = new LoadForm();

            App.CreateInstance(this);

            this.Visible = false;
            f.ShowDialog();
        }
    }
}
