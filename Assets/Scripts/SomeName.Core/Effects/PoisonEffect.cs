using System;
using SomeName.Core.Managers;

namespace SomeName.Core.Effects
{
    public class PoisonEffect : Effect
    {
        private AttackManager _attackManager;

        public PoisonEffect() { }

        public long DamagePerSecond { get; set; }

        private double _currentTime;
        private long _currentDamage;
        private long _damageDealt;

        protected override void InternalStart(IBattleUnit effectOwner)
        {
            _attackManager = new AttackManager()
            {
                AttackerDamageFactory = a => _currentDamage,
                AttackerAccuracyFactory = a => 1,
                AttackTargetEvasionFactory = a => 0,
                IsAttackerEventsActive = false,
                IsAttackTargetEventsActive = false,
                IsSoulShotActive = false
            };
            _currentTime = 0;
            _currentDamage = 0;
            _damageDealt = 0;
        }

        protected override void InternalUpdate(double timeDelta)
        {
            _currentTime += timeDelta;
            _currentDamage += Convert.ToInt64((Duration - DurationLeft) * DamagePerSecond) - _damageDealt;
            _damageDealt += _currentDamage;
            _attackManager.DealDamage(EffectOwner);
        }
    }
}
