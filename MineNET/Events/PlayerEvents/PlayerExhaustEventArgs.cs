using MineNET.Entities.Players;

namespace MineNET.Events.PlayerEvents
{
    public class PlayerExhaustEventArgs : PlayerEventArgs, ICancellable
    {
        public const int CAUSE_ATTACK = 1;
        public const int CAUSE_DAMAGE = 2;
        public const int CAUSE_MINING = 3;
        public const int CAUSE_HEALTH_REGEN = 4;
        public const int CAUSE_POTION = 5;
        public const int CAUSE_WALKING = 6;
        public const int CAUSE_SPRINTING = 7;
        public const int CAUSE_SWIMMING = 8;
        public const int CAUSE_JUMPING = 9;
        public const int CAUSE_SPRINT_JUMPING = 10;
        public const int CAUSE_CUSTOM = 11;

        public float Amount { get; set; }
        public int Cause { get; }

        public bool IsCancel { get; set; }

        public PlayerExhaustEventArgs(Player player, float amount, int cause) : base(player)
        {
            this.Amount = amount;
            this.Cause = cause;
        }
    }
}
