using System;
using SomeName.Core.Domain;
using SomeName.Core.Services;
using UnityEngine;

namespace SomeName.Core.Skills
{
    public class PowerStrike : Skill
    {
        public PowerStrike()
        {
            Description = "Power strike";
            ImageId = "PowerStrike";
        }

        public long RequiredMana { get; set; } // TODO : Доделать потребление маны.
        public double DamageKoef { get; set; }
        public long BonusDamage { get; set; }
        public double AccuracyKoef { get; set; }

        public PlayerStatsCalculator PlayerStatsCalculator = PlayerStatsCalculator.Standard;

        public override void StartCasting()
        {
            base.StartCasting();
            if (SkillService.IsCasting || CurrentCooldown > 0)
                return;
            CurrentCastingTime = 0;
            IsCasting = true;
            SkillService.IsCasting = true;
        }

        public override void StopCasting()
        {
            base.StopCasting();
            if (IsCasting)
            {
                SkillService.IsCasting = false;
                IsCasting = false;
                CurrentCooldown = Cooldown;
            }
        }

        public override void Update(IAttackTarget attackTarget, double timeDelta)
        {
            base.Update(attackTarget, timeDelta);
            if (IsCasting)
            {
                CurrentCastingTime += timeDelta;
                if (CurrentCastingTime >= CastingTime)
                {
                    DealStrike(attackTarget);
                    StopCasting();
                }
            }
            else
            {
                CurrentCooldown = CurrentCooldown > timeDelta
                    ? CurrentCooldown - timeDelta
                    : 0;
            }
        }

        // TODO : Вынести в отдельное место.
        protected void DealStrike(IAttackTarget attackTarget)
        {
            var damage = Convert.ToInt64(Attacker.GetDamage() * DamageKoef) + BonusDamage;

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
