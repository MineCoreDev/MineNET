using MineNET.Worlds.Generators.Flat;
using System.Collections.Generic;

namespace MineNET.Worlds.Generators
{
    public static class GeneratorManager
    {
        static Dictionary<string, IGenerator> generatorPool = new Dictionary<string, IGenerator>();

        static GeneratorManager()
        {
            RegisterGenerator(new FlatGenerator());
        }

        public static void RegisterGenerator(IGenerator generator)
        {
            generatorPool.Add(generator.Name, generator);
        }

        public static void UnRegisterGenerator(string name)
        {
            if (generatorPool.ContainsKey(name))
            {
                generatorPool.Remove(name);
            }
        }

        public static IGenerator GetGenerator(string name)
        {
            if (generatorPool.ContainsKey(name))
            {
                return (IGenerator) generatorPool[name].Clone();
            }

            return null;
        }
    }
}
