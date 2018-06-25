using MineNET.Commands;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MineNET.Registry
{
    public class CommandRegistry : IDictionaryRegistry<string, Command>
    {
        public Dictionary<string, Command> Dictionary { get; set; } = new Dictionary<string, Command>();

        public Command this[string key]
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

        public ICollection<string> Keys
        {
            get
            {
                return this.Dictionary.Keys;
            }
        }

        public ICollection<Command> Values
        {
            get
            {
                return this.Dictionary.Values;
            }
        }

        public void Add(KeyValuePair<string, Command> item)
        {
            this.Dictionary.Add(item.Key, item.Value);
        }

        public void Add(string key, Command value)
        {
            this.Dictionary.Add(key, value);
        }

        public void Clear()
        {
            this.Dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, Command> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            return this.Dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, Command>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, Command>> GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, Command> item)
        {
            return this.Dictionary.Remove(item.Key);
        }

        public bool Remove(string key)
        {
            return this.Dictionary.Remove(key);
        }

        public bool TryGetValue(string key, out Command value)
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
