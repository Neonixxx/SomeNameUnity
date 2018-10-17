using SomeName.Core.Balance;
using SomeName.Core.Domain;
using SomeName.Core.Monsters.Impl;
using SomeName.Core.Monsters.Interfaces;

namespace SomeName.Core.Monsters.Factories
{
    public class SimpleMonsterFactory : MonsterFactory
    {
        public override Monster Build(Level level, MonsterType monsterType = MonsterType.Normal)
        {
            var monster =  new SimpleMonster();
            var dropFactory = DropService.Standard;
            var monsterStatsBalance = MonsterStatsBalance.Get(monsterType);

            monster.Level = level.Normal;
            monster.Damage = monsterStatsBalance.GetDefaultDPS(level.Normal);
            monster.AttackSpeed = 1.0;
            monster.Accuracy = monsterStatsBalance.GetDefaultAccuracy(level.Normal);
            monster.Evasion = monsterStatsBalance.GetDefaultEvasion(level.Normal);
            monster.MaxHealth = monsterStatsBalance.GetDefaultHealth(level.Normal);
            monster.Health = monster.MaxHealth;
            monster.DroppedItems = dropFactory.Build(level.Normal, monsterStatsBalance.GetDefaultDropValue(level.Normal));
            monster.Attacker = new MonsterAttackController(monster);
            monster.MonsterType = monsterType;

            return monster;
        }
    }
}
