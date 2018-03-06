using System.Threading.Tasks;
using System.Windows.Forms;
using MineNET.Entities;

namespace MineNET.GUI.Items
{
    public partial class PlayerList : UserControl
    {
        public PlayerList()
        {
            InitializeComponent();
        }

        public async void OnUpdate()
        {
            while (!Server.Instance.IsShutdown())
            {
                Player[] players = Server.Instance.GetPlayers();
                listBox1.Items.Clear();
                for (int i = 0; i < players.Length; ++i)
                {
                    if (players[i].Name != null)
                    {
                        listBox1.Items.Add(players[i].Name);
                    }
                }

                await Task.Delay(2000);
            }
        }
    }
}
