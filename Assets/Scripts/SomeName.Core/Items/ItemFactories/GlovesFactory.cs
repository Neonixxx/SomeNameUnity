using System;
using SomeName.Core.Balance.ItemStats;

namespace SomeName.Core.Items.ItemFactories
{
    public abstract class GlovesFactory : ItemFactory
    {
        protected readonly GlovesStatsBalance GlovesStatsBalance = new GlovesStatsBalance();

        protected const double GlovesGoldValueKoef = 0.25;

        protected long GetGlovesGoldValue(int level, double damageValueKoef)
            => Convert.ToInt64(GetBaseGlovesGoldValue(level) * damageValueKoef);

        protected long GetBaseGlovesGoldValue(int level)
            => Convert.ToInt64(GetBaseItemGoldValue(level) * GlovesGoldValueKoef);
    }
}
