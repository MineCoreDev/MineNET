using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace MineNET.Utils
{
    public static class VarDump
    {
        public const int Max_Stack_Size = 10;

        public static string Var_Dump(object data)
        {
            return VarDump.Var_Dump(data, 0);
        }

        static string Var_Dump(object data, int stackSize)
        {
            StringBuilder builder = new StringBuilder();
            Type t = data.GetType();
            PropertyInfo[] props = t.GetProperties();
            FieldInfo[] fields = t.GetFields();

            if (stackSize < Max_Stack_Size)
            {
                foreach (PropertyInfo p in props)
                {
                    try
                    {
                        object value = p.GetValue(data, null);
                        string indent = string.Empty;
                        string space = "|   ";
                        string trail = "|...";

                        if (stackSize > 0)
                        {
                            indent = new StringBuilder(trail).Insert(0, space, stackSize - 1).ToString();
                        }

                        if (value != null)
                        {
                            string display = value.ToString();
                            if (value is string)
                                display = string.Concat("", display, "");
                            builder.AppendFormat("{0}{1} = {2}" + Environment.NewLine, indent, p.Name, display);
                            try
                            {
                                if (!(value is ICollection))
                                {
                                    builder.Append(VarDump.Var_Dump(value, stackSize + 1));
                                }
                                else
                                {
                                    int count = 0;
                                    foreach (object obj in (ICollection) value)
                                    {
                                        string objName = string.Format("{0}[{1}]", p.Name, count);
                                        indent = new StringBuilder(trail).Insert(0, space, stackSize).ToString();
                                        builder.AppendFormat("{0}{1} = {2}" + Environment.NewLine, indent, objName, obj.ToString());
                                        builder.Append(VarDump.Var_Dump(obj, stackSize + 2));
                                        count++;
                                    }

                                    //builder.Append(VarDump.Var_Dump(value, stackSize + 1));
                                }
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            builder.AppendFormat("{0}{1} = {2}" + Environment.NewLine, indent, p.Name, "null");
                        }
                    }
                    catch
                    {
                    }
                }

                /*foreach (FieldInfo f in fields)
                {
                    try
                    {
                        object value = f.GetValue(data);
                        string indent = string.Empty;
                        string space = "|   ";
                        string trail = "|...";

                        if (stackSize > 0)
                        {
                            indent = new StringBuilder(trail).Insert(0, space, stackSize - 1).ToString();
                        }

                        if (value != null)
                        {
                            string display = value.ToString();
                            if (value is string)
                                display = string.Concat("", display, "");
                            builder.AppendFormat("{0}{1} = {2}" + Environment.NewLine, indent, f.Name, display);
                            try
                            {
                                if (!(value is ICollection))
                                {
                                    builder.Append(VarDump.Var_Dump(value, stackSize + 1));
                                }
                                else
                                {
                                    int count = 0;
                                    foreach (object obj in (ICollection) value)
                                    {
                                        string objName = string.Format("{0}[{1}]", f.Name, count);
                                        indent = new StringBuilder(trail).Insert(0, space, stackSize).ToString();
                                        builder.AppendFormat("{0}{1} = {2}" + Environment.NewLine, indent, objName, obj.ToString());
                                        builder.Append(VarDump.Var_Dump(obj, stackSize + 2));
                                        count++;
                                    }

                                    //builder.Append(VarDump.Var_Dump(value, stackSize + 1));
                                }
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            builder.AppendFormat("{0}{1} = {2}" + Environment.NewLine, indent, f.Name, "null");
                        }
                    }
                    catch
                    {
                    }
                }*/
            }

            return builder.ToString();
        }

        /*public static string PublicFieldDump(object data)
        {
            return VarDump.PublicFieldDump(data, 0);
        }

        static string PublicFieldDump(object data, int stack)
        {
            Type type = data.GetType();
            FieldInfo[] fields = type.GetFields();
            string dump = "";
            if (stack == 0)
            {
                dump += $"[Class: {type.Name}]..." + Environment.NewLine;
            }
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
                        dump += "|-";
                        for (int i = 0; i < stack; ++i)
                        {
                            dump += "-";
                        }
                        dump += $"[Name: {fieldName}<{fieldType}>]..." + Environment.NewLine + $"{objVar}";
                    }
                    else
                    {
                        dump += "|-";
                        for (int i = 0; i < stack; ++i)
                        {
                            dump += "-";
                        }
                        dump += $"[Name: {fieldName}<{fieldType}>]{fieldValue}" + Environment.NewLine;
                    }
                }
            }

            return dump;
        }

        public static string FieldDump(object data)
        {
            return VarDump.FieldDump(data, 0);
        }

        static string FieldDump(object data, int stack)
        {
            Type type = data.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            string dump = "";
            if (stack == 0)
            {
                dump += $"[Class: {type.Name}]..." + Environment.NewLine;
            }
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
                        dump += "|-";
                        for (int i = 0; i < stack; ++i)
                        {
                            dump += "-";
                        }
                        dump += $"[Name: {fieldName}<{fieldType}>]..." + Environment.NewLine + $"{objVar}";
                    }
                    else
                    {
                        dump += "|-";
                        for (int i = 0; i < stack; ++i)
                        {
                            dump += "-";
                        }
                        dump += $"[Name: {fieldName}<{fieldType}>]{fieldValue}" + Environment.NewLine;
                    }
                }
            }

            return dump;
        }

        public static string PropertyDump(object data)
        {
            return PropertyDump(data, 0);
        }

        static string PropertyDump(object data, int stack)
        {
            Type type = data.GetType();
            PropertyInfo[] prop = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            string dump = "";
            if (stack == 0)
            {
                dump += $"[Class: {type.Name}]..." + Environment.NewLine;
            }
            foreach (PropertyInfo p in prop)
            {
                string fieldName = p.Name;
                string fieldType = p.PropertyType.Name;
                if (p.GetIndexParameters().Length == 0)
                {
                    object fieldValue = p.GetValue(data, null);

                    string objVar = VarDump.PublicFieldDump(fieldValue, stack + 1);
                    if (!string.IsNullOrEmpty(objVar))
                    {
                        dump += "|-";
                        for (int i = 0; i < stack; ++i)
                        {
                            dump += "-";
                        }
                        dump += $"[Name: {fieldName}<{fieldType}>]..." + Environment.NewLine + $"{objVar}";
                    }
                    else
                    {
                        dump += "|-";
                        for (int i = 0; i < stack; ++i)
                        {
                            dump += "-";
                        }
                        dump += $"[Name: {fieldName}<{fieldType}>]{fieldValue}" + Environment.NewLine;
                    }
                }
            }

            return dump;
        }*/
    }
}
