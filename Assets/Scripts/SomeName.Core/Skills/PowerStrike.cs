using System;
using SomeName.Core.Domain;
using SomeName.Core.Managers;
using SomeName.Core.Services;

namespace SomeName.Core.Skills
{
    public class PowerStrike : ActiveSkill
    {
        public PowerStrike()
        {
            Description = "Power strike";
            ImageId = "PowerStrike";
        }

        protected override void InitializeInternal(SkillService skillService, IBattleUnit attacker)
        {
            _attackManager = new AttackManager(attacker)
            {
                AttackerDamageFactory = a => Convert.ToInt64(a.GetDamage() * DamageKoef) + BonusDamage
            };
        }

        public long RequiredMana { get; set; } // TODO : Доделать потребление маны.
        public double DamageKoef { get; set; }
        public long BonusDamage { get; set; }
        public double AccuracyKoef { get; set; }

        private AttackManager _attackManager;

        protected override void DoSkill(IBattleUnit attackTarget, double timeDelta)
            => _attackManager.DealDamage(attackTarget);
    }
}
