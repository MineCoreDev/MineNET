using System;

namespace MineNET.Utils
{
    public static class ArrayUtils
    {
        public static T[] CreateArray<T>(int size, T defaultValue)
        {
            T[] array = (T[]) Array.CreateInstance(typeof(T), size);
            for (int i = 0; i < size; ++i)
            {
                array[i] = defaultValue;
            }

            return array;
        }

        public static byte[] CreateRandomByteArray(int size, byte max, byte min)
        {
            byte[] array = (byte[]) Array.CreateInstance(typeof(byte), size);
            for (int i = 0; i < size; ++i)
            {
                array[i] = (byte) new System.Random().Next(min, max);
            }

            return array;
        }

        public static T[] CreateArray<T>(int size) where T : new()
        {
            T[] array = (T[]) Array.CreateInstance(typeof(T), size);
            for (int i = 0; i < size; ++i)
            {
                array[i] = new T();
            }

            return array;
        }

        public static NibbleArray CreateNibbleArray(int size)
        {
            return new NibbleArray(size);
        }

        public static NibbleArray CreateNibbleArray(int size, byte defaultValue)
        {
            NibbleArray na = ArrayUtils.CreateNibbleArray(size);
            for (int i = 0; i < size; ++i)
            {
                na[i] = defaultValue;
            }

            return na;
        }
    }

    public class NibbleArray
    {
        byte[] arrayData;
        public byte[] ArrayData
        {
            get
            {
                return this.arrayData;
            }

            set
            {
                this.arrayData = value;
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
                return (byte) (this.arrayData[index >> 1] >> ((index & 1) * 4) & 0xf);
            }

            set
            {
                value &= 0xf;
                int idx = index >> 1;
                this.arrayData[idx] &= (byte) (0xf << (((index + 1) & 1) * 4));
                this.arrayData[idx] |= (byte) (value << ((index & 1) * 4));
            }
        }

        public NibbleArray()
        {
        }

        public NibbleArray(int size)
        {
            this.arrayData = new byte[size / 2];
        }

        public NibbleArray(byte[] data)
        {
            this.arrayData = data;
        }
    }
}
