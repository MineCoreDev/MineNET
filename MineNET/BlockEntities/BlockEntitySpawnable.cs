using MineNET.Entities.Players;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.BlockEntities
{
    public abstract class BlockEntitySpawnable : BlockEntity
    {
        public BlockEntitySpawnable(Chunk chunk, CompoundTag nbt) : base(chunk, nbt)
        {

        }

        protected override void Init(CompoundTag nbt)
        {
            base.Init(nbt);

            this.SpawnToAll();
        }

        public void SpawnToAll()
        {
            Player[] players = this.World.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                this.SpawnTo(players[i]);
            }
        }

        public void SpawnTo(Player player)
        {
            BlockEntityDataPacket pk = new BlockEntityDataPacket
            {
                Position = new BlockCoordinate3D((int) this.X, (int) this.Y, (int) this.Z),
                Namedtag = NBTIO.WriteTag(this.SaveNBT(), NBTEndian.LITTLE_ENDIAN, true)
            };
            player.SendPacket(pk);
        }
    }
}
