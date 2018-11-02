using System;
using SomeName.Core.Domain;

namespace SomeName.Core
{
    public class AttackManager
    {
        public AttackManager(IAttacker attacker)
        {
            Attacker = attacker;
        }

        public PlayerStatsCalculator PlayerStatsCalculator = PlayerStatsCalculator.Standard;

        public IAttacker Attacker { get; private set; }

        public Func<IAttacker, long> AttackerDamageFactory { get; set; } = a => a.GetDamage();

        public Func<IAttacker, int> AttackerAccuracyFactory { get; set; } = a => a.GetAccuracy();

        public Func<IAttacker, double> AttackerCritChanceFactory { get; set; } = a => a.GetCritChance();

        public Func<IAttacker, double> AttackerCritDamageFactory { get; set; } = a => a.GetCritDamage();

        public Func<IAttackTarget, int> AttackTargetEvasionFactory { get; set; } = a => a.GetEvasion();

        public Func<IAttackTarget, double> AttackTargetDefenceKoefFactory { get; set; } = a => a.GetDefenceKoef();

        public long DealDamage(IAttackTarget attackTarget)
        {
            var attackerAccuracy = AttackerAccuracyFactory(Attacker);
            var attackTargetEvasion = AttackTargetEvasionFactory(attackTarget);

            if (!IsHitSuccesful(attackerAccuracy, attackTargetEvasion))
            {
                attackTarget.OnEvadeActivate(Attacker, null);
                return 0;
            }

            var attackerDamage = AttackerDamageFactory(Attacker);
            var attackerCritChance = AttackerCritChanceFactory(Attacker);
            if (Dice.TryGetChance(attackerCritChance))
            {
                var attackerCritDamage = AttackerCritDamageFactory(Attacker);
                attackerDamage = Convert.ToInt64(attackerDamage * attackerCritDamage);
            }

            var damageDealt = DealDamage(attackTarget, attackerDamage);
            Attacker.OnHit();
            return damageDealt;
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

        protected long GetDealtDamage(IAttackTarget attackTarget, long damage)
        {
            var attackTargetDefenceKoef = AttackTargetDefenceKoefFactory(attackTarget);
            var dealtDamage = Convert.ToInt64(damage * (1 - attackTargetDefenceKoef));
            return attackTarget.Health - dealtDamage >= 0
                ? dealtDamage
                : attackTarget.Health;
        }
    }
}
