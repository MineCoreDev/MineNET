using MineNET.Blocks;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MineNET.Registry
{
    public class BlockRegistry : IDictionaryRegistry<int, Block>
    {
        private Dictionary<int, Block> Dictionary { get; set; } = new Dictionary<int, Block>();

        public Block this[int key]
        {
            get
            {
                return this.Dictionary[key].Clone();
            }

            set
            {
                this.Dictionary[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return this.Dictionary.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<int> Keys
        {
            get
            {
                return this.Dictionary.Keys;
            }
        }

        public ICollection<Block> Values
        {
            get
            {
                return this.Dictionary.Values;
            }
        }

        public void Add(KeyValuePair<int, Block> item)
        {
            this.Dictionary.Add(item.Key, item.Value);
        }

        public void Add(int key, Block value)
        {
            this.Dictionary.Add(key, value);
        }

        public void Clear()
        {
            this.Dictionary.Clear();
        }

        public bool Contains(KeyValuePair<int, Block> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(int key)
        {
            return this.Dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<int, Block>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<int, Block>> GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }

        public bool Remove(KeyValuePair<int, Block> item)
        {
            return this.Dictionary.Remove(item.Key);
        }

        public bool Remove(int key)
        {
            return this.Dictionary.Remove(key);
        }

        public bool TryGetValue(int key, out Block value)
        {
            bool result = this.Dictionary.TryGetValue(key, out value);
            value = value.Clone();
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }
    }
}
