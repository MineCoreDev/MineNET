using System;
using System.Collections;
using System.Collections.Generic;

namespace MineNET.Registry
{
    public abstract class DictionaryRegistryBase<TKey, TValue> : IDictionaryRegistry<TKey, TValue>
    {
        protected Dictionary<TKey, TValue> Dictionary { get; } = new Dictionary<TKey, TValue>();

        public virtual TValue this[TKey key]
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

        public virtual int Count => this.Dictionary.Count;

        public virtual bool IsReadOnly => false;

        public virtual ICollection<TKey> Keys => this.Dictionary.Keys;

        public virtual ICollection<TValue> Values => this.Dictionary.Values;

        public virtual void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Dictionary.Add(item.Key, item.Value);
        }

        public virtual void Add(TKey key, TValue value)
        {
            this.Dictionary.Add(key, value);
        }

        public virtual void Clear()
        {
            this.Dictionary.Clear();
        }

        public virtual bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public virtual bool ContainsKey(TKey key)
        {
            return this.Dictionary.ContainsKey(key);
        }

        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }

        public virtual bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Dictionary.Remove(item.Key);
        }

        public virtual bool Remove(TKey key)
        {
            return this.Dictionary.Remove(key);
        }

        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            return this.Dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }
    }
}