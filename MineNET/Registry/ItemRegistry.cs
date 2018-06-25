using MineNET.Items;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MineNET.Registry
{
    public class ItemRegistry : IDictionaryRegistry<int, Item>
    {
        private Dictionary<int, Item> Dictionary { get; set; } = new Dictionary<int, Item>();

        public Item this[int key]
        {
            get
            {
                return this.Dictionary[key];
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

        public ICollection<Item> Values
        {
            get
            {
                return this.Dictionary.Values;
            }
        }

        public void Add(KeyValuePair<int, Item> item)
        {
            this.Dictionary.Add(item.Key, item.Value);
        }

        public void Add(int key, Item value)
        {
            this.Dictionary.Add(key, value);
        }

        public void Clear()
        {
            this.Dictionary.Clear();
        }

        public bool Contains(KeyValuePair<int, Item> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(int key)
        {
            return this.Dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<int, Item>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<int, Item>> GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }

        public bool Remove(KeyValuePair<int, Item> item)
        {
            return this.Dictionary.Remove(item.Key);
        }

        public bool Remove(int key)
        {
            return this.Dictionary.Remove(key);
        }

        public bool TryGetValue(int key, out Item value)
        {
            return this.Dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }
    }
}
