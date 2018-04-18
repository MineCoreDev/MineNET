using MineNET.Commands.Data;
using MineNET.Commands.Enums;
using MineNET.Commands.Parameters;
using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Data;
using MineNET.Utils;

namespace MineNET.Commands.Defaults
{
    public class EffectCommand : Command
    {
        public override string Name
        {
            get
            {
                return "effect";
            }
        }

        public override string Description
        {
            get
            {
                return "%commands.effect.description";
            }
        }

        public override PlayerPermissions CommandPermission
        {
            get
            {
                return PlayerPermissions.OPERATOR;
            }
        }

        public override CommandOverload[] CommandOverloads
        {
            get
            {
                return new CommandOverload[]
                {
                    new CommandOverload(
                        new CommandParameterTarget("player", false),
                        new CommandParameterValueEnum("clear")
                    ),
                    new CommandOverload(
                        new CommandParameterTarget("player", false),
                        new CommandParameterString("effect", false),//TODO
                        new CommandParameterInt("seconds", true),
                        new CommandParameterInt("amplifier", true),
                        new CommandParameterString("bool", true, new CommandEnumBoolean())
                    )
                };
            }
        }

        public override bool Execute(CommandSender sender, params string[] args)
        {
            if (args.Length < 2)
            {
                sender.SendMessage("/effect [target] clear");
                sender.SendMessage("/effect [target] [effect] [seconds] [amplifier] [true:false]");
                return false;
            }

            if (args[0] == "@e")
            {
                this.SendTargetNotPlayerMessage(sender);
                return false;
            }

            Entity[] entities = this.GetEntityFromSelector(args[0], sender);
            if (entities == null)
            {
                this.SendNoTargetMessage(sender);
                return false;
            }

            if (args[1] == "clear")
            {
                for (int i = 0; i < entities.Length; ++i)
                {
                    if (entities[i] is EntityLiving)
                    {
                        EntityLiving entity = (EntityLiving) entities[i];
                        if (entity.GetEffects().Length > 0)
                        {
                            entity.RemoveAllEffect();
                            sender.SendMessage(new TranslationMessage("commands.effect.success.removed.all", entity.Name));
                        }
                        else
                        {
                            sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.effect.failure.notActive.all", entity.Name));
                        }
                    }
                    else
                    {
                        sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.effect.failure.notAMob", entities[i].Name));
                    }
                }
                return true;
            }

            Effect effect = Effect.GetEffect(args[1]);
            if (effect == null)
            {
                this.SendSyntaxMessage(sender);
                return false;
            }

            int seconds = 20 * 300;
            if (args.Length > 2)
            {
                if (!int.TryParse(args[2], out seconds))
                {
                    this.SendSyntaxMessage(sender);
                    return false;
                }
            }
            effect.Duration = seconds;

            int amplifier = 0;
            if (args.Length > 3)
            {
                if (!int.TryParse(args[3], out amplifier))
                {
                    this.SendSyntaxMessage(sender);
                    return false;
                }
            }
            effect.Amplifier = amplifier;

            bool visible = true;
            if (args.Length > 4)
            {
                if (!bool.TryParse(args[4], out visible))
                {
                    this.SendSyntaxMessage(sender);
                    return false;
                }
            }
            effect.Visible = visible;

            for (int i = 0; i < entities.Length; ++i)
            {
                if (entities[i] is EntityLiving)
                {
                    EntityLiving entity = (EntityLiving) entities[i];
                    entity.AddEffect(effect.Clone());
                    sender.SendMessage(new TranslationMessage("commands.effect.success", effect.Name, effect.Amplifier, entity.Name, effect.Duration));
                }
                else
                {
                    sender.SendMessage(new TranslationMessage(ColorText.RED, "commands.effect.failure.notAMob", entities[i].Name));
                }
            }
            return true;
        }
    }
}
