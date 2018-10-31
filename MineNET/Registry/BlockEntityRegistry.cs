using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using MineNET.BlockEntities;
using MineNET.NBT.Tags;
using MineNET.Worlds;

namespace MineNET.Registry
{
    public class BlockEntityRegistry : DictionaryRegistryBase<string, Type>
    {
        private readonly Dictionary<string, Func<Chunk, CompoundTag, BlockEntity>> _expressionCache = new Dictionary<string, Func<Chunk, CompoundTag, BlockEntity>>();

        public override Type this[string key]
        {
            get
            {
                return this.Dictionary[key];
            }

            set
            {
                this.Dictionary[key] = value;
                this.CreateExpression(key, value);
            }
        }

        public override void Add(string key, Type value)
        {
            base.Add(key, value);

            this.CreateExpression(key, value);
        }

        public override void Add(KeyValuePair<string, Type> item)
        {
            base.Add(item);

            this.CreateExpression(item.Key, item.Value);
        }

        public override bool Remove(string key)
        {
            this._expressionCache.Remove(key);

            return base.Remove(key);
        }

        public override bool Remove(KeyValuePair<string, Type> item)
        {
            this._expressionCache.Remove(item.Key);

            return base.Remove(item);
        }

        public override void Clear()
        {
            base.Clear();

            this._expressionCache.Clear();
        }

        public void UpdateExpression()
        {
            this._expressionCache.Clear();
            foreach (KeyValuePair<string, Type> pair in this.Dictionary)
            {
                this.CreateExpression(pair.Key, pair.Value);
            }
        }

        public Func<Chunk, CompoundTag, BlockEntity> GetExpression(string key)
        {
            if (this.ContainsKey(key))
            {
                return this._expressionCache[key];
            }
            else
            {
                throw new KeyNotFoundException(key);
            }
        }

        private void CreateExpression(string key, Type t)
        {
            var constructor = t.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder,
                new[] { typeof(Chunk), typeof(CompoundTag) }, null);
            var p1 = Expression.Parameter(typeof(Chunk));
            var p2 = Expression.Parameter(typeof(CompoundTag));

            var lamda = Expression.Lambda<Func<Chunk, CompoundTag, BlockEntity>>(Expression.New(constructor, p1, p2), p1, p2).Compile();
            this._expressionCache.Add(key, lamda);
        }
    }
}
