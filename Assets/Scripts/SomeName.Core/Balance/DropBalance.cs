﻿using SomeName.Core.Domain;
using static System.Convert;
using static System.Math;

namespace SomeName.Core.Balance
{
    public class DropBalance
    {
        public PlayerStatsBalance PlayerStatsBalance { get; set; }

        public static readonly double DropExpValueKoef = 1.0;
        public static readonly double DropGoldValueKoef = 0.3;
        public static readonly double DropItemsValueKoef = 1 - DropGoldValueKoef;


        public long GetExp(Level level)
            => GetExp(level.Normal, level.Paragon);

        public long GetExp(int level, int paragonLevel = -1)
        {
            var paragonKoef = paragonLevel == -1
                ? 1.0
                : 0.3 + 0.006 * paragonLevel;
            return ToInt64(GetBaseDropValuePerLevel(level).Exp * paragonKoef);
        }

        public long GetBaseItemValue(int level)
            => ToInt64(GetBaseDropValuePerLevel(level).Items / Pow(level, 0.25) + GetBaseDropValuePerLevel(1).Items * 0.2);

        private DropValue GetBaseDropValuePerLevel(int level)
            => GetBaseDropValuePerSecond(level)
                .ApplyKoef(GetSecondsForLevel(level));

        public long GetSecondsForLevel(int level)
            => ToInt64(Pow(level, 2.5) / 50 + 30);


        public DropValue GetDropValue(int level, long seconds)
            => GetDropValuePerSecond(level).ApplyKoef(seconds);

        public DropValue GetDropValuePerSecond(int level)
            => GetBaseDropValuePerSecond(level)
                .ApplyKoef(PlayerStatsBalance.GetDefaultDPSKoef(level));

        private DropValue GetBaseDropValuePerSecond(int level)
        {
            var baseDropValuePerSecond = Pow(level, 2.15) + 20;
            
            return new DropValue
            {
                Exp = ToInt64(baseDropValuePerSecond * DropExpValueKoef),
                Gold = ToInt64(baseDropValuePerSecond * DropGoldValueKoef),
                Items = ToInt64(baseDropValuePerSecond * DropItemsValueKoef)
            };
        }

        public static readonly DropBalance Standard = new DropBalance
        {
            PlayerStatsBalance = PlayerStatsBalance.Standard
        };
    }
}
