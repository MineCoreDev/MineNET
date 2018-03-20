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
            if (comment != null && !string.IsNullOrEmpty(comment.Comment))
            {
                if (comment.Comment[0] == '%')
                {
                    if (comment.Manager != null)
                    {
                        string msg = comment.Manager.GetString(comment.Comment.Remove(0, 1));
                        context.Emit(new Comment(msg, false));
                    }
                    else
                    {
                        string msg = LangManager.GetString(comment.Comment.Remove(0, 1));
                        context.Emit(new Comment(msg, false));
                    }
                }
                else
                {
                    context.Emit(new Comment(comment.Comment, false));
                }
            }

            return base.EnterMapping(key, value, context);
        }
    }
}