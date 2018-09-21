using System.Collections;
using System.Collections.Generic;

namespace MineNET.Registry
{
    public class ListRegistryBase<T> : IListRegistry<T>
    {
        private List<T> List { get; } = new List<T>();

        public T this[int index]
        {
            get
            {
                return this.List[index];
            }

            set
            {
                this.List[index] = value;
            }
        }

        public int Count => this.List.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            this.List.Add(item);
        }

        public void Clear()
        {
            this.List.Clear();
        }

        public bool Contains(T item)
        {
            return this.List.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.List.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return this.List.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.List.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return this.List.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this.List.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.List.GetEnumerator();
        }
    }
}