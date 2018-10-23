using System;
using SomeName.Core.Balance;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class ScrollOfEnchantArmorFactory : ChestFactory
    {
        public override long GetItemGoldValue(int level)
            => Convert.ToInt64(GetBaseChestGoldValue(GetLevel(level)) * 0.20);

        public override Item Build(int level, double additionalKoef = 1.0)
        {
            var newLevel = GetLevel(level);
            var item = new ScrollOfEnchantArmor()
            {
                Level = newLevel,
            };
            item.GoldValue.Base = GetItemGoldValue(newLevel);
            item.UpdateGoldValueKoef();

            return item;
        }

        private int GetLevel(int level)
            => Math.Max(level, PlayerStatsBalance.MaxLevel);
    }
}
