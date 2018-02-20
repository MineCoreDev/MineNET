namespace MineNET.Utils
{
    public static class ArrayUtil
    {
        public static T[] CreateArray<T>(int size, T defaultValue = default(T))
        {
            T[] array = new T[size];
            for (int i = 0; i < size; ++i)
            {
                array[i] = defaultValue;
            }

            return array;
        }

        public static NibbleArray CreateNibbleArray(int size)
        {
            return new NibbleArray(size);
        }
    }

    public class NibbleArray
    {
        byte[] arrayData;
        public byte[] ArrayData
        {
            get
            {
                return arrayData;
            }

            set
            {
                arrayData = value;
            }
        }

        public int Length
        {
            get
            {
                return this.ArrayData.Length * 2;
            }
        }

        public byte this[int index]
        {
            get
            {
                return (byte) (arrayData[index >> 1] >> ((index & 1) * 4) & 0xf);
            }

            set
            {
                value &= 0xf;
                int idx = index >> 1;
                arrayData[idx] &= (byte) (0xf << (((index + 1) & 1) * 4));
                arrayData[idx] |= (byte) (value << ((index & 1) * 4));
            }
        }

        public NibbleArray()
        {
        }

        public NibbleArray(int size)
        {
            this.arrayData = new byte[size / 2];
        }
    }
}
