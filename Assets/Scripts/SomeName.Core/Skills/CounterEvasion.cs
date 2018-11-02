using System;
using SomeName.Core.Domain;
using SomeName.Core.Services;

namespace SomeName.Core.Skills
{
    public class CounterEvasion : Skill
    {
        public override void Initialize(SkillService skillService, IAttacker attacker)
        {
            base.Initialize(skillService, attacker);
            _attackManager = new AttackManager(attacker)
            {
                AttackerDamageFactory = a => Convert.ToInt64(a.GetEvasion() * DamageKoef) + BonusDamage
            };
        }

        public double DamageKoef = 1.0;
        public long BonusDamage = 0;
        public double AccuracyKoef = 1.0;
        public double TriggerChance;

        private AttackManager _attackManager;
        private IAttackTarget _attackTarget;

        public override void StartBattle()
        {
            base.StartBattle();
            Attacker.OnEvade += Attack;
        }

        public override void Update(IAttackTarget attackTarget, double timeDelta)
        {
            base.Update(attackTarget, timeDelta);
            _attackTarget = attackTarget;
        }

        public override void EndBattle()
        {
            base.EndBattle();
            Attacker.OnEvade -= Attack;
        }

        private void Attack(object obj, EventArgs e)
        {
            if (!Dice.TryGetChance(TriggerChance))
                return;
            _attackManager.DealDamage(_attackTarget);
        }
    }
}
