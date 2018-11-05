using System;
using SomeName.Core.Domain;
using SomeName.Core.Managers;
using SomeName.Core.Services;

namespace SomeName.Core.Skills
{
    public class PowerStrike : Skill
    {
        public PowerStrike()
        {
            Description = "Power strike";
            ImageId = "PowerStrike";
        }

        public override void Initialize(SkillService skillService, IAttacker attacker)
        {
            base.Initialize(skillService, attacker);
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

        protected void DealStrike(IAttackTarget attackTarget)
            => _attackManager.DealDamage(attackTarget);
    }
}
