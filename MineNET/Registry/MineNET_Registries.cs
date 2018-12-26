using MineNET.Registry;

namespace MineNET
{
    public static class MineNET_Registries
    {
        public static BlockEntityRegistry BlockEntity { get; } = new BlockEntityRegistry();
        public static BlockRegistry Block { get; } = new BlockRegistry();
        public static CommandRegistry Command { get; } = new CommandRegistry();
        public static CreativeItemRegistry Creative { get; } = new CreativeItemRegistry();
        public static EffectRegistry Effect { get; } = new EffectRegistry();
        public static EntityIdentityRegistry EntityIdentity { get; } = new EntityIdentityRegistry();
        public static EntityRegistry Entity { get; } = new EntityRegistry();
        public static ItemRegistry Item { get; } = new ItemRegistry();
        public static RakNetPacketRegistry RakNetPacket { get; } = new RakNetPacketRegistry();
        public static MinecraftPacketRegistry MinecraftPacket { get; } = new MinecraftPacketRegistry();

        public static void Init()
        {
            MineNET_Registries.BlockEntity.Clear();
            MineNET_Registries.Block.Clear();
            MineNET_Registries.Command.Clear();
            MineNET_Registries.Creative.Clear();
            MineNET_Registries.Effect.Clear();
            MineNET_Registries.EntityIdentity.Clear();
            MineNET_Registries.Entity.Clear();
            MineNET_Registries.Item.Clear();
            MineNET_Registries.RakNetPacket.Clear();
            MineNET_Registries.MinecraftPacket.Clear();
        }

    }
}
