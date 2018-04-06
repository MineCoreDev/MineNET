using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MineNET.GUI.Forms
{
    public partial class VersionForm : Form
    {
        public VersionForm()
        {
            InitializeComponent();
        }

        private void VersionForm_Load(object sender, EventArgs e)
        {
            label2.Text = $"Version: {Application.ProductVersion}";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;

            Process.Start("https://github.com/MineNETDevelopmentGroup/MineNET");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel2.LinkVisited = true;

            Process.Start("https://github.com/aaubry/YamlDotNet");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel3.LinkVisited = true;

            Process.Start("https://github.com/JamesNK/Newtonsoft.Json");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel4.LinkVisited = true;

            Process.Start("https://github.com/icsharpcode/SharpZipLib");
        }
    }
}
