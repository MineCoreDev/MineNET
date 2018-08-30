using System;
using System.Resources;

namespace MineNET.Utils.Config
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class YamlDescriptionAttribute : Attribute
    {
        public YamlDescriptionAttribute(string description, Type t = null)
        {
            this.Description = description;
            if (t != null)
            {
                this.Manager = new ResourceManager(t);
            }
        }

        public YamlDescriptionAttribute(string description, Type target, string resourceName, bool useFullName = false)
        {
            this.Description = description;
            if (useFullName)
            {
                this.Manager = new ResourceManager(resourceName, target.Assembly);
            }
            else
            {
                this.Manager = new ResourceManager($"{resourceName}", target.Assembly);
            }

        }

        public string Description
        {
            get;
        }

        public ResourceManager Manager
        {
            get;
        }
    }
}
