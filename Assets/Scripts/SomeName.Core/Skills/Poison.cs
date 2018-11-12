using System;
using SomeName.Core.Effects;
using SomeName.Core.Services;

namespace SomeName.Core.Skills
{
    public class Poison : ActiveSkill
    {
        public double Duration { get; set; }
        public long BonusDamagePerSecond { get; set; } = 0;
        public double DamagePerSecondKoef { get; set; } = 1.0;
        private PoisonEffect _poisonEffect;

        public Poison()
        {
            ImageId = "PoisonEffect";
            Description = "Poison";
        }

        protected override void InitializeInternal(SkillService skillService, IBattleUnit attacker)
        {
            _poisonEffect = new PoisonEffect()
            {
                Duration = Duration,
                ImageId = "PoisonEffect"
            };
        }

        protected override void DoSkill(IBattleUnit attackTarget, double timeDelta)
        {
            _poisonEffect.DamagePerSecond = Convert.ToInt64(Attacker.GetDamage() * DamagePerSecondKoef + BonusDamagePerSecond);
            attackTarget.EffectService.Add(_poisonEffect);
        }
    }
}
