using System;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace MineNET.Utils.Config.Yaml
{
    public sealed class CommentObjectDescriptor : IObjectDescriptor
    {
        public readonly IObjectDescriptor innerDescriptor;

        public CommentObjectDescriptor(IObjectDescriptor innerDescriptor, string comment, bool isInline)
        {
            this.innerDescriptor = innerDescriptor;
            this.Comment = comment;
            this.IsInline = isInline;
        }

        public string Comment
        {
            get;
            private set;
        }

        public bool IsInline
        {
            get;
            private set;
        }

        public ScalarStyle ScalarStyle
        {
            get
            {
                return innerDescriptor.ScalarStyle;
            }
        }

        public Type StaticType
        {
            get
            {
                return innerDescriptor.StaticType;
            }
        }

        public Type Type
        {
            get
            {
                return innerDescriptor.Type;
            }
        }

        public object Value
        {
            get
            {
                return innerDescriptor.Value;
            }
        }
    }
}
