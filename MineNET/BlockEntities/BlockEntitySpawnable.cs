using MineNET.Entities.Players;
using MineNET.IO;
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
                Namedtag = NBTIO.WriteTag(this.SpawnCompound(), NBTEndian.LITTLE_ENDIAN, true)
            };
            player.SendPacket(pk);
        }

        public virtual CompoundTag SpawnCompound()
        {
            CompoundTag nbt = new CompoundTag();
            nbt.PutString("id", this.Name);
            nbt.PutInt("x", (int) this.X);
            nbt.PutInt("y", (int) this.Y);
            nbt.PutInt("z", (int) this.Z);

            return nbt;
        }
    }
}