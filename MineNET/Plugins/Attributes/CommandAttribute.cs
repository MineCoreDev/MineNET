using System;

namespace MineNET.Plugins.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CommandAttribute : Attribute
    {
    }
}
