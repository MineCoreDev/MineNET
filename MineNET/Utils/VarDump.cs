using System;
using System.Reflection;

namespace MineNET.Utils
{
    public static class VarDump
    {
        public static string PublicFieldDump(object data, int stack = 0)
        {
            Type type = data.GetType();
            FieldInfo[] fields = type.GetFields();
            string dump = "";
            foreach (FieldInfo field in fields)
            {
                if (!field.IsStatic)
                {
                    string fieldName = field.Name;
                    string fieldType = field.FieldType.Name;
                    object fieldValue = field.GetValue(data);

                    string objVar = VarDump.PublicFieldDump(fieldValue, stack + 1);
                    if (!string.IsNullOrEmpty(objVar))
                    {
                        for (int i = 0; i < stack; ++i)
                        {
                            dump += "  ";
                        }
                        dump += $"[Name: {fieldName}<{fieldType}>]..." + Environment.NewLine + $"{objVar}";
                    }
                    else
                    {
                        for (int i = 0; i < stack; ++i)
                        {
                            dump += "  ";
                        }
                        dump += $"[Name: {fieldName}<{fieldType}>]{fieldValue}" + Environment.NewLine;
                    }
                }
            }

            return dump;
        }
    }
}
