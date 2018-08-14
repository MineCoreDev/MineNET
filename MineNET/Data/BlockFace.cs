using MineNET.Values;

namespace MineNET.Data
{
    public enum BlockFace
    {
        DOWN = 0,
        UP = 1,
        NORTH = 2,
        SOUTH = 3,
        WEST = 4,
        EAST = 5,
    }

    public static class BlockFaceExtensions
    {
        public static BlockFace FromIndex(int index)
        {
            if (index == 0)
            {
                return BlockFace.DOWN;
            }
            else if (index == 1)
            {
                return BlockFace.UP;
            }
            else if (index == 2)
            {
                return BlockFace.NORTH;
            }
            else if (index == 3)
            {
                return BlockFace.SOUTH;
            }
            else if (index == 4)
            {
                return BlockFace.WEST;
            }
            else if (index == 5)
            {
                return BlockFace.EAST;
            }
            return BlockFace.DOWN;
        }

        public static int GetIndex(this BlockFace face)
        {
            return (int) face;
        }

        public static int GetDirection(this BlockFace face)
        {
            if (face == BlockFace.NORTH)
            {
                return 3;
            }
            else if (face == BlockFace.SOUTH)
            {
                return 0;
            }
            else if (face == BlockFace.WEST)
            {
                return 1;
            }
            else if (face == BlockFace.EAST)
            {
                return 3;
            }
            else
            {
                return -1;
            }
        }

        public static Vector3 GetPosition(this BlockFace face)
        {
            if (face == BlockFace.DOWN)
            {
                return new Vector3(0, -1, 0);
            }
            else if (face == BlockFace.UP)
            {
                return new Vector3(0, 1, 0);
            }
            else if (face == BlockFace.NORTH)
            {
                return new Vector3(0, 0, -1);
            }
            else if (face == BlockFace.SOUTH)
            {
                return new Vector3(0, 0, 1);
            }
            else if (face == BlockFace.WEST)
            {
                return new Vector3(-1, 0, 0);
            }
            else if (face == BlockFace.EAST)
            {
                return new Vector3(1, 0, 0);
            }
            else
            {
                return new Vector3(0, 0, 0);
            }
        }

        public static BlockFace GetReverseBlockFace(this BlockFace face)
        {

            if (face == BlockFace.DOWN)
            {
                return BlockFace.UP;
            }
            else if (face == BlockFace.UP)
            {
                return BlockFace.DOWN;
            }
            else if (face == BlockFace.NORTH)
            {
                return BlockFace.SOUTH;
            }
            else if (face == BlockFace.SOUTH)
            {
                return BlockFace.NORTH;
            }
            else if (face == BlockFace.WEST)
            {
                return BlockFace.EAST;
            }
            else if (face == BlockFace.EAST)
            {
                return BlockFace.WEST;
            }
            else
            {
                return BlockFace.UP;
            }
        }
    }
}