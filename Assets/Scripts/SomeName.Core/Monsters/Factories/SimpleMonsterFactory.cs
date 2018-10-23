using SomeName.Core.Balance;
using SomeName.Core.Domain;
using SomeName.Core.Monsters.Impl;
using SomeName.Core.Monsters.Interfaces;
using SomeName.Core.Skills;

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
            monster.Accuracy = monsterStatsBalance.GetDefaultAccuracy(level.Normal);
            monster.Evasion = monsterStatsBalance.GetDefaultEvasion(level.Normal);
            monster.MaxHealth = monsterStatsBalance.GetDefaultHealth(level.Normal);
            monster.Health = monster.MaxHealth;
            monster.DroppedItems = dropFactory.Build(level.Normal, monsterStatsBalance.GetDefaultDropValue(level.Normal));
            monster.MonsterType = monsterType;

            monster.Skills.DefaultSkill = new PowerStrike() { CastingTime = 0.5, DamageKoef = 1, AccuracyKoef = 1.0, Cooldown = 0.5 };

            if (monsterType == MonsterType.Boss && level.Normal > 50)
                monster.Skills.ActiveSkills.Add(new PowerStrike() { CastingTime = 0.9, DamageKoef = 4, AccuracyKoef = 1.5, Cooldown = 8 });

            monster.MonsterSkillController = new MonsterSkillController(monster, monster.Skills);

            return monster;
        }
    }
}
