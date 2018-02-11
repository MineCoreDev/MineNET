using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.ObjectGraphVisitors;

namespace MineNET.Utils.Config.Yaml
{
    class CommentObjectGraphVisitor : ChainedObjectGraphVisitor
    {
        public CommentObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor) : base(nextVisitor)
        {
        }

        public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context)
        {
            CommentObjectDescriptor comment = value as CommentObjectDescriptor;
            if (comment != null && comment.Comment != null)
            {
                context.Emit(new Comment(comment.Comment, false));
            }

            return base.EnterMapping(key, value, context);
        }
    }
}
