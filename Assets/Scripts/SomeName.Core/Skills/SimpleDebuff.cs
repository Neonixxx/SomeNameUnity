using SomeName.Core.Effects;
using SomeName.Core.Services;

namespace SomeName.Core.Skills
{
    public class SimpleDebuf : ActiveSkill
    {
        public Effect Effect;

        public SimpleDebuf(string imageId, string description)
        {
            ImageId = imageId;
            Description = description;
        }

        protected override void DoSkill(IBattleUnit attackTarget, double timeDelta)
        {
            attackTarget.EffectService.Add(Effect);
        }

        public static SimpleDebuf GetDamageDebuff(double damageKoef, double duration, long damageBonus = 0)
            => new SimpleDebuf("DamageDebuff", "Слабость")
            {
                Effect = new Effect
                {
                    ImageId = "DamageDebuff",
                    Duration = duration,
                    DamageKoef = damageKoef,
                    DamageBonus = damageBonus
                }
            };

        public static SimpleDebuf GetDefenceDebuff(double defenceKoef, double duration, long defenceBonus = 0)
            => new SimpleDebuf("DefenceDebuff", "Разрушение брони")
            {
                Effect = new Effect
                {
                    ImageId = "DefenceDebuff",
                    Duration = duration,
                    DefenceKoef = defenceKoef,
                    DefenceBonus = defenceBonus
                }
            };
    }
}
