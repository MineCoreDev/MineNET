using MineNET.Events.IOEvents;
using MineNET.Events.NetworkEvents;
using MineNET.Events.PlayerEvents;
using MineNET.Events.ServerEvents;

namespace MineNET.Events
{
    public sealed class EventManager
    {
        public IOEvent IO { get; }
        public NetworkEvent Network { get; }
        public PlayerEvent Player { get; }
        public ServerEvent Server { get; }

        public EventManager()
        {
            this.IO = new IOEvent();
            this.Network = new NetworkEvent();
            this.Player = new PlayerEvent();
            this.Server = new ServerEvent();
        }
    }
}
