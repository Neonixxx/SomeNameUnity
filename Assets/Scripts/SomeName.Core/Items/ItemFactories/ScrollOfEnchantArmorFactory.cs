using System;
using SomeName.Core.Balance;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class ScrollOfEnchantArmorFactory : ChestFactory
    {
        public override long GetItemGoldValue(int level)
            => Convert.ToInt64(GetBaseChestGoldValue(GetItemLevel(level)) * 0.20);

        public override Item Build(int level)
        {
            var newLevel = GetItemLevel(level);
            var item = new ScrollOfEnchantArmor()
            {
                Level = newLevel,
                Quantity = 1,
            };
            item.GoldValue.Base = GetItemGoldValue(newLevel);
            item.UpdateGoldValueKoef();

            return item;
        }

        protected override int GetItemLevel(int level)
            => PlayerStatsBalance.MaxLevel;
    }
}
