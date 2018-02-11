using System;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace MineNET.Utils.Config.Yaml
{
    public sealed class CommentPropertyDescriptor : IPropertyDescriptor
    {
        private readonly IPropertyDescriptor baseDescriptor;

        public CommentPropertyDescriptor(IPropertyDescriptor baseDescriptor)
        {
            this.baseDescriptor = baseDescriptor;
            Name = baseDescriptor.Name;
        }

        public string Name
        {
            get;
            set;
        }

        public Type Type
        {
            get
            {
                return baseDescriptor.Type;
            }
        }

        public Type TypeOverride
        {
            get
            {
                return baseDescriptor.TypeOverride;
            }

            set
            {
                baseDescriptor.TypeOverride = value;
            }
        }

        public int Order
        {
            get;
            set;
        }

        public ScalarStyle ScalarStyle
        {
            get
            {
                return baseDescriptor.ScalarStyle;
            }

            set
            {
                baseDescriptor.ScalarStyle = value;
            }
        }

        public bool CanWrite
        {
            get
            {
                return baseDescriptor.CanWrite;
            }
        }

        public void Write(object target, object value)
        {
            baseDescriptor.Write(target, value);
        }

        public T GetCustomAttribute<T>() where T : Attribute
        {
            return GetCustomAttribute<T>();
        }

        public IObjectDescriptor Read(object target)
        {
            YamlDescriptionAttribute att = baseDescriptor.GetCustomAttribute<YamlDescriptionAttribute>();
            return att != null ? new CommentObjectDescriptor(baseDescriptor.Read(target), att.Description, att.IsInline) : baseDescriptor.Read(target);
        }
    }
}
