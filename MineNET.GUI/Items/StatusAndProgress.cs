using System.Windows.Forms;

namespace MineNET.GUI.Items
{
    public partial class StatusAndProgress : UserControl
    {
        public StatusAndProgress()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return label1.Text;
            }

            set
            {
                label1.Text = value;
            }
        }

        public ProgressBar ProgressBar
        {
            get
            {
                return progressBar1;
            }
        }
    }
}
