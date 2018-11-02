using System;
using SomeName.Core.Balance.ItemStats;

namespace SomeName.Core.Items.ItemFactories
{
    public abstract class ChestFactory : ItemFactory
    {
        protected readonly ChestStatsBalance ChestStatsBalance = new ChestStatsBalance();

        protected const double ChestGoldValueKoef = 0.3;

        protected long GetChestGoldValue(int level, double damageValueKoef)
            => Convert.ToInt64(GetBaseChestGoldValue(level) * damageValueKoef);

        protected long GetBaseChestGoldValue(int level)
            => Convert.ToInt64(GetBaseItemGoldValue(level) * ChestGoldValueKoef);
    }
}
