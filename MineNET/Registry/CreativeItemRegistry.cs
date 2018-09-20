using System.Collections;
using System.Collections.Generic;
using MineNET.Items;

namespace MineNET.Registry
{
    public class CreativeItemRegistry : IListRegistry<ItemStack>
    {
        private List<ItemStack> List { get; set; } = new List<ItemStack>();

        public ItemStack this[int index]
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

        public int Count
        {
            get
            {
                return this.List.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(ItemStack item)
        {
            this.List.Add(item);
        }

        public void Clear()
        {
            this.List.Clear();
        }

        public bool Contains(ItemStack item)
        {
            return this.List.Contains(item);
        }

        public void CopyTo(ItemStack[] array, int arrayIndex)
        {
            this.List.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ItemStack> GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        public int IndexOf(ItemStack item)
        {
            return this.List.IndexOf(item);
        }

        public void Insert(int index, ItemStack item)
        {
            this.List.Insert(index, item);
        }

        public bool Remove(ItemStack item)
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
