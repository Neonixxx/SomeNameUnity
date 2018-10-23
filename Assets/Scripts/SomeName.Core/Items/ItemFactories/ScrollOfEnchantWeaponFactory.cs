using System;
using SomeName.Core.Balance;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class ScrollOfEnchantWeaponFactory : WeaponFactory
    {
        public override long GetItemGoldValue(int level)
            => Convert.ToInt64(GetBaseWeaponGoldValue(GetLevel(level)) * 0.30);

        public override Item Build(int level, double additionalKoef = 1.0)
        {
            var newLevel = GetLevel(level);
            var item = new ScrollOfEnchantWeapon()
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
