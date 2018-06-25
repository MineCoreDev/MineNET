using MineNET.Registry;

namespace MineNET
{
    public static class MineNET_Registries
    {
        //BlockEntity
        public static BlockRegistry Block { get; } = new BlockRegistry();
        public static CommandRegistry Command { get; } = new CommandRegistry();
        public static EffectRegistry Effect { get; } = new EffectRegistry();
        public static EntityRegistry Entity { get; } = new EntityRegistry();
        public static ItemRegistry Item { get; } = new ItemRegistry();
        public static RakNetPacketRegistry RakNetPacket { get; } = new RakNetPacketRegistry();
        public static MinecraftPacketRegistry MinecraftPacket { get; } = new MinecraftPacketRegistry();

        public static void Init()
        {
            //BlockEntity
            MineNET_Registries.Block.Clear();
            MineNET_Registries.Command.Clear();
            MineNET_Registries.Effect.Clear();
            MineNET_Registries.Item.Clear();
            MineNET_Registries.RakNetPacket.Clear();
            MineNET_Registries.MinecraftPacket.Clear();
        }

    }
}
