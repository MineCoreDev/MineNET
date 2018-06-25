using MineNET.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MineNET.Registry
{
    public class EffectRegistry : IDictionaryRegistry<int, Effect>
    {
        public Dictionary<int, Effect> Dictionary { get; set; } = new Dictionary<int, Effect>();

        public Effect this[int key]
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

        public ICollection<Effect> Values
        {
            get
            {
                return this.Dictionary.Values;
            }
        }

        public void Add(KeyValuePair<int, Effect> item)
        {
            this.Dictionary.Add(item.Key, item.Value);
        }

        public void Add(int key, Effect value)
        {
            this.Dictionary.Add(key, value);
        }

        public void Clear()
        {
            this.Dictionary.Clear();
        }

        public bool Contains(KeyValuePair<int, Effect> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(int key)
        {
            return this.Dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<int, Effect>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<int, Effect>> GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }

        public bool Remove(KeyValuePair<int, Effect> item)
        {
            return this.Dictionary.Remove(item.Key);
        }

        public bool Remove(int key)
        {
            return this.Dictionary.Remove(key);
        }

        public bool TryGetValue(int key, out Effect value)
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
