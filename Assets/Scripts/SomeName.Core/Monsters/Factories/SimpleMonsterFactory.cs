using System.Linq;
using SomeName.Core.Balance;
using SomeName.Core.Domain;
using SomeName.Core.Monsters.Impl;
using SomeName.Core.Monsters.Interfaces;
using SomeName.Core.Services;
using SomeName.Core.Skills;

namespace SomeName.Core.Monsters.Factories
{
    public class SimpleMonsterFactory : MonsterFactory
    {
        private readonly ISkill[] _possibleBossActiveSkills;

        public SimpleMonsterFactory()
        {
            var debuffCooldown = 25.0;
            var debuffCastingTime = 2.5;

            var damageDebuff = SimpleDebuf.GetDamageDebuff(0.65, 15);
            damageDebuff.Cooldown = debuffCooldown;
            damageDebuff.CastingTime = debuffCastingTime;

            var defenceDebuff = SimpleDebuf.GetDefenceDebuff(0.65, 15);
            defenceDebuff.Cooldown = debuffCooldown;
            defenceDebuff.CastingTime = debuffCastingTime;

            _possibleBossActiveSkills = new ISkill[]
            {
                new PowerStrike() { DamageKoef = 4, AccuracyKoef = 1.5, CastingTime = 1.6, Cooldown = 8 },
                new Poison() { DamagePerSecondKoef = 0.7, Duration = 5, CastingTime = debuffCastingTime, Cooldown = 13 },
                damageDebuff,
                defenceDebuff
            };
        }

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

            monster.Skills.DefaultSkill = new AutoAttackSkill { Cooldown = 0.3, CastingTime = 0.7 };

            if (monsterType == MonsterType.Boss && level.Normal > 50)
            {
                var skillsCount = 1;
                if (level.Normal > 80)
                    skillsCount++;
                if (level.Normal > 110)
                    skillsCount++;
                _possibleBossActiveSkills.TakeRandom(skillsCount)
                    .ToList()
                    .ForEach(s => monster.Skills.ActiveSkills.Add(s));
            }

            monster.SkillService = new MonsterSkillController(monster, monster.Skills);
            monster.EffectService = new EffectService(monster, monster.Effects);

            return monster;
        }
    }
}
