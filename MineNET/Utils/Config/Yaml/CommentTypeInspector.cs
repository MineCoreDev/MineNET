using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeInspectors;

namespace MineNET.Utils.Config.Yaml
{
    public class CommentTypeInspector : TypeInspectorSkeleton
    {
        private readonly ITypeInspector innerTypeDescriptor;

        public CommentTypeInspector(ITypeInspector inspector)
        {
            if (inspector == null)
            {
                throw new ArgumentNullException();
            }
            this.innerTypeDescriptor = inspector;
        }

        public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
        {
            return this.innerTypeDescriptor
                .GetProperties(type, container)
                .Select(d => new CommentPropertyDescriptor(d));
        }
    }
}
