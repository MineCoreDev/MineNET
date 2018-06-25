using System;
using System.Collections;
using System.Collections.Generic;

namespace MineNET.Worlds.Rule
{
    public class GameRules : ICollection<GameRuleBase>, ICollection
    {
        List<GameRuleBase> gameRules = new List<GameRuleBase>();

        public int Count
        {
            get
            {
                return ((ICollection<GameRuleBase>) this.gameRules).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((ICollection<GameRuleBase>) this.gameRules).IsReadOnly;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return ((ICollection) this.gameRules).IsSynchronized;
            }
        }

        public object SyncRoot
        {
            get
            {
                return ((ICollection) this.gameRules).SyncRoot;
            }
        }

        public void Add(GameRuleBase item)
        {
            ((ICollection<GameRuleBase>) this.gameRules).Add(item);
        }

        public void Clear()
        {
            ((ICollection<GameRuleBase>) this.gameRules).Clear();
        }

        public bool Contains(GameRuleBase item)
        {
            return ((ICollection<GameRuleBase>) this.gameRules).Contains(item);
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection) this.gameRules).CopyTo(array, index);
        }

        public void CopyTo(GameRuleBase[] array, int arrayIndex)
        {
            ((ICollection<GameRuleBase>) this.gameRules).CopyTo(array, arrayIndex);
        }

        public IEnumerator<GameRuleBase> GetEnumerator()
        {
            return ((ICollection<GameRuleBase>) this.gameRules).GetEnumerator();
        }

        public bool Remove(GameRuleBase item)
        {
            return ((ICollection<GameRuleBase>) this.gameRules).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<GameRuleBase>) this.gameRules).GetEnumerator();
        }

        public GameRuleBase this[int index]
        {
            get
            {
                return this.gameRules[index];
            }

            set
            {
                this.gameRules[index] = value;
            }
        }
    }
}
