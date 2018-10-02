using MineNET.BlockEntities;
using MineNET.NBT.Tags;
using MineNET.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Registry
{
    public class BlockEntityRegistry : DictionaryRegistryBase<string, Type>
    {
        private readonly Dictionary<string, Func<Chunk, CompoundTag, BlockEntity>> _expressionCache = new Dictionary<string, Func<Chunk, CompoundTag, BlockEntity>>();

        public override void Add(string key, Type value)
        {
            base.Add(key, value);

            CreateExpression(key, value);
        }

        public override void Add(KeyValuePair<string, Type> item)
        {
            base.Add(item);

            CreateExpression(item.Key, item.Value);
        }

        public override bool Remove(string key)
        {
            _expressionCache.Remove(key);

            return base.Remove(key);
        }

        public override bool Remove(KeyValuePair<string, Type> item)
        {
            _expressionCache.Remove(item.Key);

            return base.Remove(item);
        }

        public override void Clear()
        {
            base.Clear();

            _expressionCache.Clear();
        }

        public void UpdateExpression()
        {
            _expressionCache.Clear();
            foreach (KeyValuePair<string, Type> pair in Dictionary)
            {
                CreateExpression(pair.Key, pair.Value);
            }
        }

        public Func<Chunk, CompoundTag, BlockEntity> GetExpression(string key)
        {
            if (ContainsKey(key))
            {
                return _expressionCache[key];
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

            var lamda = Expression.Lambda<Func<Chunk, CompoundTag, Entity>>(Expression.New(constructor, p1, p2), p1, p2).Compile();
            _expressionCache.Add(key, lamda);
        }
    }
}
