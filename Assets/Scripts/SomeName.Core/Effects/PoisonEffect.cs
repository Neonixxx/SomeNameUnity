using System;
using SomeName.Core.Managers;
using UnityEngine;

namespace SomeName.Core.Effects
{
    public class PoisonEffect : Effect
    {
        private AttackManager _attackManager;

        public PoisonEffect() { }

        public long DamagePerSecond { get; set; }

        private double _currentTime;

        private long _currentDamage;

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
        }

        protected override void InternalUpdate(double timeDelta)
        {
            _currentTime = timeDelta;
            _currentDamage = Convert.ToInt64(_currentTime * DamagePerSecond);
            _attackManager.DealDamage(EffectOwner);
        }
    }
}
