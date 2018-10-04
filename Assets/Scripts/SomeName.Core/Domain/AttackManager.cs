using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core.Domain
{
    public class AttackManager
    {
        public IAttacker Attacker { get; set; }

        public PlayerStatsCalculator PlayerStatsCalculator = PlayerStatsCalculator.Standard;

        public AttackManager(IAttacker attacker)
        {
            Attacker = attacker;
        }

        public long Attack(IAttackTarget attackTarget)
        {
            var damage = Attacker.GetDamage();

            if (!IsHitSuccesful(Attacker.GetAccuracy(), attackTarget.GetEvasion()))
                return 0;

            var critChance = Attacker.GetCritChance();
            if (Dice.TryGetChance(critChance))
                damage = Convert.ToInt64(damage * Attacker.GetCritDamage());

            var damageDealt = DealDamage(attackTarget, damage);
            Attacker.OnHit();
            
            return damageDealt;
        }

        private bool IsHitSuccesful(int accuracy, int evasion)
        {
            var hitChance = PlayerStatsCalculator.CalculateHitChance(accuracy, evasion);
            return Dice.TryGetChance(hitChance);
        }

        private long DealDamage(IAttackTarget attackTarget, long damage)
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
