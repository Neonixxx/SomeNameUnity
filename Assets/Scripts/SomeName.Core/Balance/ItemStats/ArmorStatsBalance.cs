using SomeName.Core.Domain;
using static System.Convert;
using static System.Math;

namespace SomeName.Core.Balance.ItemStats
{
    public abstract class ArmorStatsBalance : ItemStatsBalance
    {
        protected abstract double DefenceKoef { get; }

        public MainStat<long> GetDefence(int level, double damageValueKoef)
            => new MainStat<long> { Base = GetBaseDefence(level), Koef = damageValueKoef, EnchantKoef = 1.0 };

        private long GetBaseDefence(int level)
            => ToInt64(DefenceKoef * (20 * Pow(E, 0.04 * level) - 10));
    }
}
