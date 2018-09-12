using SomeName.Core.Domain;
using SomeName.Core.Monsters.Interfaces;
using System;
using System.Threading;

namespace SomeName.Core
{
    public class MonsterAttackController
    {
        public Monster Monster { get; set; }

        public AttackManager AttackManager { get; set; }

        private double _attackCooldown;

        private double _currentCooldown;

        public MonsterAttackController(Monster monster)
        {
            Monster = monster;
            AttackManager = new AttackManager(monster);
            _attackCooldown = 1.0 / monster.AttackSpeed;
            _currentCooldown = _attackCooldown;
        }

        public void Update(IAttackTarget attackTarget, double timeDelta)
        {
            _currentCooldown -= timeDelta;

            if (_currentCooldown <= 0)
            {
                _currentCooldown = _attackCooldown;
                AttackManager.Attack(attackTarget);
            }
        }
    }
}