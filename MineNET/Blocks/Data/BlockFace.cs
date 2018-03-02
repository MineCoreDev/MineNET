using MineNET.Values;

namespace MineNET.Blocks.Data
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
            else if (face == BlockFace.WEST)
            {
                return new Vector3(1, 0, 0);
            }
            else
            {
                return new Vector3(0, 0, 0);
            }
        }
    }
}
