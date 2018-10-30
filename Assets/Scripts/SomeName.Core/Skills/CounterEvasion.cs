using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeName.Core.Domain;

namespace SomeName.Core.Skills
{
    public class CounterEvasion : Skill
    {
        private PlayerStatsCalculator PlayerStatsCalculator = PlayerStatsCalculator.Standard;
        private IAttackTarget _attackTarget;

        public double DamageKoef = 1.0;
        public long BonusDamage = 0;
        public double AccuracyKoef = 1.0;
        public double TriggerChance;

        // TODO : Переименовать StartBattle и EndBattle.
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
            DealStrike(_attackTarget);
        }

        // Вынести в отдельное место.
        protected void DealStrike(IAttackTarget attackTarget)
        {
            var damage = Convert.ToInt64(Attacker.GetEvasion() * DamageKoef) + BonusDamage;

            if (!IsHitSuccesful(Convert.ToInt32(Attacker.GetAccuracy() * AccuracyKoef), attackTarget.GetEvasion()))
            {
                attackTarget.OnEvadeActivate(Attacker, null);
                return;
            }

            var critChance = Attacker.GetCritChance();
            if (Dice.TryGetChance(critChance))
                damage = Convert.ToInt64(damage * Attacker.GetCritDamage());

            var damageDealt = DealDamage(attackTarget, damage);
            Attacker.OnHit();
        }

        protected bool IsHitSuccesful(int accuracy, int evasion)
        {
            var hitChance = PlayerStatsCalculator.CalculateHitChance(accuracy, evasion);
            return Dice.TryGetChance(hitChance);
        }

        protected long DealDamage(IAttackTarget attackTarget, long damage)
        {
            var dealtDamage = GetDealtDamage(attackTarget, damage);
            attackTarget.Health -= dealtDamage;
            if (attackTarget.Health == 0)
                attackTarget.IsDead = true;
            return dealtDamage;
        }

        private long GetDealtDamage(IAttackTarget attackTarget, long damage)
        {
            var dealtDamage = Convert.ToInt64(damage * (1 - attackTarget.GetDefenceKoef()));
            return attackTarget.Health - dealtDamage >= 0
                ? dealtDamage
                : attackTarget.Health;
        }
    }
}
