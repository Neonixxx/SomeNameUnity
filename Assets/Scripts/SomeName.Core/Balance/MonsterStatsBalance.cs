using SomeName.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;

namespace SomeName.Core.Balance
{
    public class MonsterStatsBalance
    {
        public PlayerStatsBalance PlayerStatsBalance { get; set; }

        public DropBalance DropBalance { get; set; }

        public PlayerStatsCalculator PlayerStatsCalculator { get; set; }


        public DropValue GetDefaultDropValue(int level)
            => DropBalance.GetDropValue(level, GetDefaultBattleLength(level));

        public long GetDefaultTouchness(int level)
            => ToInt64(GetDefaultHealth(level) / (1 - GetDefaultEvadeChance(level)));

        public long GetDefaultHealth(int level)
            => ToInt64(PlayerStatsBalance.GetDefaultDPS(level) * GetDefaultBattleLength(level)
                * (1 - GetDefaultEvadeChance(level)));

        public long GetDefaultDPS(int level)
            => ToInt64(PlayerStatsBalance.GetDefaultToughness(level) / GetDefaultBattleLength(level)
                / GetDefaultHitChance(level)) + PlayerStatsBalance.GetDefaultTouchnessPerSecond(level);

        public long GetDefaultBattleLength(int level)
            => ToInt64(ToDouble(DropBalance.GetSecondsForLevel(level)) / GetMonstersForLevel(level));

        public double GetDefaultHitChance(int level)
            => PlayerStatsCalculator.CalculateHitChance(GetDefaultAccuracy(level), GetDefaultEvasion(level));

        public double GetDefaultEvadeChance(int level)
            => PlayerStatsCalculator.CalculateHitChance(GetDefaultEvasion(level), GetDefaultAccuracy(level));

        public int GetDefaultAccuracy(int level)
            => PlayerStatsBalance.GetDefaultEvasion(level);

        public int GetDefaultEvasion(int level)
            => PlayerStatsBalance.GetDefaultAccuracy(level);


        private static int GetMonstersForLevel(int level)
            => ToInt32(level / 2.0 + 1);


        public static readonly MonsterStatsBalance Standard = new MonsterStatsBalance
        {
            PlayerStatsBalance = PlayerStatsBalance.Standard,
            DropBalance = DropBalance.Standard,
            PlayerStatsCalculator = PlayerStatsCalculator.Standard
        };
    }
}
