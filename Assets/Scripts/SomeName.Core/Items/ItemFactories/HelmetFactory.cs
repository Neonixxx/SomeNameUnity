using System;
using SomeName.Core.Balance.ItemStats;

namespace SomeName.Core.Items.ItemFactories
{
    public abstract class HelmetFactory : ItemFactory
    {
        protected readonly HelmetStatsBalance HelmetStatsBalance = new HelmetStatsBalance();

        protected const double HelmetGoldValueKoef = 0.30;

        protected long GetHelmetGoldValue(int level, double damageValueKoef)
            => Convert.ToInt64(GetBaseHelmetGoldValue(level) * damageValueKoef);

        protected long GetBaseHelmetGoldValue(int level)
            => Convert.ToInt64(GetBaseItemGoldValue(level) * HelmetGoldValueKoef);
    }
}
