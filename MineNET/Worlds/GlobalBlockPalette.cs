using MineNET.Resources;
using MineNET.Utils;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace MineNET.Worlds
{
    public static class GlobalBlockPalette
    {
        private static Dictionary<int, int> _runtimeFromLegacy = new Dictionary<int, int>();
        private static Dictionary<int, int> _legacyFromRuntime = new Dictionary<int, int>();
        public static byte[] PaletteBytes { get; private set; }

        public static void Init()
        {
            string json = Encoding.UTF8.GetString(Resource.runtimeid_table);
            JArray table = JArray.Parse(json);
            int count = table.Count;
            BinaryStream stream = new BinaryStream();
            stream.WriteUVarInt((uint) count);

            for (int i = 0; i < count; i++)
            {
                JObject data = (JObject) table[i];
                int id = data.Value<int>("id");
                int damage = data.Value<int>("data");
                string name = data.Value<string>("name");
                int runtime = i;
                int legacy = (id << 4) | damage;

                stream.WriteString(name);
                stream.WriteLShort((ushort) damage);

                _runtimeFromLegacy.Add(runtime, legacy);
                _legacyFromRuntime.Add(legacy, runtime);
            }

            PaletteBytes = stream.ToArray();
        }

        public static int GetRuntimeID(int id, int damage)
        {
            int legacy = (id << 4) | damage;

            return GetRuntimeID(legacy);
        }

        public static int GetRuntimeID(int legacy)
        {
            if (_legacyFromRuntime.ContainsKey(legacy))
            {
                return _legacyFromRuntime[legacy];
            }

            return -1;
        }

        public static int GetLegacyID(int runtimeID)
        {
            if (_runtimeFromLegacy.ContainsKey(runtimeID))
            {
                return _runtimeFromLegacy[runtimeID];
            }

            return -1;
        }

        public static void Clear()
        {
            _runtimeFromLegacy.Clear();
            _legacyFromRuntime.Clear();
        }
    }
}