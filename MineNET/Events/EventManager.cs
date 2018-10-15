using MineNET.Events.BlockEvents;
using MineNET.Events.EntityEvents;
using MineNET.Events.InventoryEvents;
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
        public EntityEvent Entity { get; }
        public ServerEvent Server { get; }
        public BlockEvent Block { get; }
        public InventoryEvent Inventory { get; }

        public EventManager()
        {
            this.IO = new IOEvent();
            this.Network = new NetworkEvent();
            this.Player = new PlayerEvent();
            this.Entity = new EntityEvent();
            this.Server = new ServerEvent();
            this.Block = new BlockEvent();
            this.Inventory = new InventoryEvent();
        }
    }
}
