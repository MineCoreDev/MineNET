using System;
using MineNET.BlockEntities;
using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Inventories;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Blocks
{
    public class BlockChest : BlockContainer
    {
        public BlockChest() : base(BlockIDs.CHEST, "Chest")
        {
            this.Hardness = 2.5f;
            this.Resistance = 12.5f;
            this.ToolType = ItemToolType.AXE;

            this.CanBeActivated = true;
        }

        public override bool Place(Block clicked, Block replace, BlockFace face, Vector3 clickPos, Player player, ItemStack item)
        {
            int[] faces = { 2, 5, 3, 4};
            this.Damage = faces[player.GetDirection().GetHorizontalIndex()];
            this.Position.World.SetBlock((Vector3)this.Position, this, true);

            CompoundTag nbt = new CompoundTag();
            nbt.PutList(new ListTag(NBTTagType.COMPOUND));
            nbt.PutInt("x", (int)this.Position.X);
            nbt.PutInt("y", (int)this.Position.Y);
            nbt.PutInt("z", (int)this.Position.Z);

            Chunk chunk = this.Position.World.GetChunk(new Tuple<int, int>((int) this.Position.X >> 4, (int) this.Position.Z >> 4));

            BlockEntity.CreateBlockEntity("chest", chunk, nbt);
            return true;
        }

        public override bool Activate(Player player, ItemStack item)
        {
            BlockEntity blockEntity = this.Position.World.GetBlockEntity(new BlockCoordinate3D(this.Position.X, this.Position.Y, this.Position.Z));
            if (!(blockEntity is BlockEntityChest))
            {
                CompoundTag nbt = new CompoundTag();
                nbt.PutList(new ListTag(NBTTagType.COMPOUND));
                nbt.PutInt("x", (int)this.Position.X);
                nbt.PutInt("y", (int)this.Position.Y);
                nbt.PutInt("z", (int)this.Position.Z);

                Chunk chunk = this.Position.World.GetChunk(new Tuple<int, int>((int)this.Position.X >> 4, (int)this.Position.Z >> 4));

                blockEntity = BlockEntity.CreateBlockEntity("chest", chunk, nbt);
            }

            Inventory inventory = ((BlockEntityChest) blockEntity).Inventory;
            player.Inventory.OpenInventory(inventory);
            return true;
        }

        public override bool Break(Player player, ItemStack item)
        {
            return base.Break(player, item);
        }
    }
}
