using System.IO;
using System.Runtime.Serialization;

namespace MineNET.Extensions
{
    public static class ObjectExtension
    {
        public static object FastClone(this object obj, System.Type type)
        {
            object result;
            DataContractSerializer f = new DataContractSerializer(type);
            MemoryStream m = new MemoryStream();

            try
            {
                f.WriteObject(m, obj);
                m.Position = 0;
                result = f.ReadObject(m);
            }
            finally
            {
                m.Close();
            }

            return result;
        }

        public static object StaticFastClone(this object obj)
        {
            object result;
            DataContractSerializer f = new DataContractSerializer(typeof(object));
            MemoryStream m = new MemoryStream();

            try
            {
                f.WriteObject(m, obj);
                m.Position = 0;
                result = f.ReadObject(m);
            }
            finally
            {
                m.Close();
            }

            return result;
        }

        public static T StaticFastClone<T>(this object obj)
        {
            T result;
            DataContractSerializer f = new DataContractSerializer(typeof(T));
            MemoryStream m = new MemoryStream();

            try
            {
                f.WriteObject(m, obj);
                m.Position = 0;
                result = (T)f.ReadObject(m);
            }
            finally
            {
                m.Close();
            }

            return result;
        }
    }
}
