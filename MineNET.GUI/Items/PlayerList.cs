using System.Collections.Generic;
using System.Windows.Forms;
using MineNET.Entities.Players;
using MineNET.Utils;

namespace MineNET.GUI.Items
{
    public partial class PlayerList : UserControl
    {
        public PlayerList()
        {
            InitializeComponent();

            ClockConstantController.CreateController("gui_playerlist", 2000);
        }

        public async void OnUpdate()
        {
            while (!Server.Instance.IsShutdown())
            {
                ClockConstantController.Start("gui_playerlist");

                Player[] players = Server.Instance.GetPlayers();
                List<string> list = new List<string>();
                List<string> online = new List<string>();
                for (int i = 0; i < players.Length; ++i)
                {
                    if (!string.IsNullOrEmpty(players[i].Name))
                    {
                        if (!listBox1.Items.Contains(players[i].Name))
                        {
                            listBox1.Items.Add(players[i].Name);
                        }
                    }
                    list.Add(players[i].Name);
                }

                foreach (object obj in listBox1.Items)
                {
                    online.Add(obj.ToString());
                }

                for (int i = 0; i < online.Count; ++i)
                {
                    if (!list.Contains(online[i]))
                    {
                        if (listBox1.Items.Contains(online[i]))
                        {
                            listBox1.Items.Remove(online[i]);
                        }
                    }
                }

                await ClockConstantController.Stop("gui_playerlist");
            }
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

        internal ListBox PlayerListBox
        {
            get
            {
                return this.listBox1;
            }
        }
    }
}
