using SomeName.Core.Difficulties;
using SomeName.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;
using static System.Math;

namespace SomeName.Core.Balance
{
    public class DropBalance
    {
        public static readonly double DropExpValueKoef = 1.0;

        public static readonly double DropGoldValueKoef = 0.3;

        public static readonly double DropItemsValueKoef = 1 - DropGoldValueKoef;


        public double GetExpKoef()
            => BattleDifficulty.GetCurrent().ExpMultiplier;

        public double GetGoldKoef()
            => BattleDifficulty.GetCurrent().GoldMultiplier;

        public double GetItemsKoef()
            => BattleDifficulty.GetCurrent().DropMultiplier;


        public long GetExp(int level)
            => GetBaseDropValuePerLevel(level).Exp;

        public long GetBaseItemValue(int level)
            => GetBaseDropValuePerLevel(level).Items;

        private DropValue GetBaseDropValuePerLevel(int level)
            => GetBaseDropValuePerSecond(level)
                .ApplyKoef(GetSecondsForLevel(level));

        public long GetSecondsForLevel(int level)
            => ToInt64(Pow(level, 3) / 500 + 30);


        public DropValue GetDropValue(int level, long seconds)
            => GetDropValuePerSecond(level).ApplyKoef(seconds);

        public DropValue GetDropValuePerSecond(int level)
            => GetBaseDropValuePerSecond(level)
                .ApplyKoefs(GetExpKoef(), GetGoldKoef(), GetItemsKoef());

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

        public static readonly DropBalance Standard = new DropBalance();
    }
}
