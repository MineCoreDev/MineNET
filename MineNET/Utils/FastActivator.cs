using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MineNET.Utils
{
    public static class FastActivator<R, P1, P2>
    {
        private static Func<P1, P2, R> _cache;

        public static Func<P1, P2, R> CreateInstance(Type t)
        {
            if (_cache == null)
            {
                var constructor = t.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder,
                                new[] { typeof(P1), typeof(P2) }, null);
                var p1 = Expression.Parameter(typeof(P1));
                var p2 = Expression.Parameter(typeof(P2));

                _cache = Expression.Lambda<Func<P1, P2, R>>(Expression.New(constructor, p1, p2), p1, p2).Compile();
            }

            return _cache;
        }
    }
}