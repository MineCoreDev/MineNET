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
            this.Name = baseDescriptor.Name;
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
                return this.baseDescriptor.Type;
            }
        }

        public Type TypeOverride
        {
            get
            {
                return this.baseDescriptor.TypeOverride;
            }

            set
            {
                this.baseDescriptor.TypeOverride = value;
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
                return this.baseDescriptor.ScalarStyle;
            }

            set
            {
                this.baseDescriptor.ScalarStyle = value;
            }
        }

        public bool CanWrite
        {
            get
            {
                return this.baseDescriptor.CanWrite;
            }
        }

        public void Write(object target, object value)
        {
            this.baseDescriptor.Write(target, value);
        }

        public T GetCustomAttribute<T>() where T : Attribute
        {
            return GetCustomAttribute<T>();
        }

        public IObjectDescriptor Read(object target)
        {
            YamlDescriptionAttribute att = this.baseDescriptor.GetCustomAttribute<YamlDescriptionAttribute>();
            return att != null ? new CommentObjectDescriptor(this.baseDescriptor.Read(target), att.Description, att.IsInline) : this.baseDescriptor.Read(target);
        }
    }
}
