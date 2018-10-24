using System;
using System.Collections.Generic;
using SomeName.Core.Domain;
using SomeName.Core.Monsters.Interfaces;
using static System.Convert;

namespace SomeName.Core.Balance
{
    public class MonsterStatsBalance
    {
        public PlayerStatsBalance PlayerStatsBalance { get; set; }

        public DropBalance DropBalance { get; set; }

        public PlayerStatsCalculator PlayerStatsCalculator { get; set; }

        public Func<int, double> HealthPercentPerMonster { get; set; }


        // TODO : Сделать дроп для элитных монстров и боссов.
        public DropValue GetDefaultDropValue(int level)
            => DropBalance.GetDropValue(level, GetDefaultBattleLength(level));

        public long GetDefaultTouchness(int level)
            => ToInt64(GetDefaultHealth(level) / (1 - GetDefaultEvadeChance(level)));

        public long GetDefaultHealth(int level)
            => ToInt64(PlayerStatsBalance.GetDefaultDPS(level) * GetBaseBattleLength(level)
                * (1 - GetDefaultEvadeChance(level)) * GetHealthKoef(level));

        public long GetDefaultDPS(int level)
        {
            var playerBattleTouchness = ToDouble(PlayerStatsBalance.GetDefaultToughness(level)) / GetBaseBattleLength(level) 
                + PlayerStatsBalance.GetDefaultTouchnessPerSecond(level);
            var dpsKoef = GetBaseHealthPercentPerMonster(level) * GetDPSKoef(level) / GetDefaultHitChance(level);
            return ToInt64(playerBattleTouchness * dpsKoef);
        }

        public long GetDefaultBattleLength(int level)
            => ToInt64(GetBaseBattleLength(level) * GetHealthKoef(level));

        public double GetDefaultHitChance(int level)
            => PlayerStatsCalculator.CalculateHitChance(GetDefaultAccuracy(level), GetDefaultEvasion(level));

        public double GetDefaultEvadeChance(int level)
            => PlayerStatsCalculator.CalculateHitChance(GetDefaultEvasion(level), GetDefaultAccuracy(level));

        public int GetDefaultAccuracy(int level)
            => PlayerStatsBalance.GetDefaultEvasion(level);

        public int GetDefaultEvasion(int level)
            => PlayerStatsBalance.GetDefaultAccuracy(level);


        public static long GetBaseBattleLength(int level)
            => ToInt64(GetBaseHealthPercentPerMonster(level) * 100 * GetSecondsPerPlayerHealthPercent(level));

        /// <summary>
        /// Получить процент здоровья, который снимает монстр игроку за битву.
        /// </summary>
        private static double GetBaseHealthPercentPerMonster(int level)
            => 0.1 + level * 0.0003;

        private double GetDPSKoef(int level)
            => Math.Pow(GetHealthPercentKoef(level), 0.25);

        private double GetHealthKoef(int level)
            => Math.Pow(GetHealthPercentKoef(level), 0.75);

        private double GetHealthPercentKoef(int level)
            => HealthPercentPerMonster(level) / GetBaseHealthPercentPerMonster(level);

        private static double GetSecondsPerPlayerHealthPercent(int level)
            => Math.Pow(level, 2) / 4000 + 0.3;


        public static MonsterStatsBalance Get(MonsterType monsterType)
            => MonsterStatsBalances[monsterType];

        private static readonly Dictionary<MonsterType, MonsterStatsBalance> MonsterStatsBalances = new Dictionary<MonsterType, MonsterStatsBalance>
        {
            [MonsterType.Normal] = new MonsterStatsBalance
            {
                PlayerStatsBalance = PlayerStatsBalance.Standard,
                DropBalance = DropBalance.Standard,
                PlayerStatsCalculator = PlayerStatsCalculator.Standard,
                HealthPercentPerMonster = level => GetBaseHealthPercentPerMonster(level),
            },
            [MonsterType.Elite] = new MonsterStatsBalance
            {
                PlayerStatsBalance = PlayerStatsBalance.Standard,
                DropBalance = DropBalance.Standard,
                PlayerStatsCalculator = PlayerStatsCalculator.Standard,
                HealthPercentPerMonster = level => 0.3 + level * 0.002,
            },
            [MonsterType.Boss] = new MonsterStatsBalance
            {
                PlayerStatsBalance = PlayerStatsBalance.Standard,
                DropBalance = DropBalance.Standard,
                PlayerStatsCalculator = PlayerStatsCalculator.Standard,
                HealthPercentPerMonster = level => 1,
            },
        };
    }
}
