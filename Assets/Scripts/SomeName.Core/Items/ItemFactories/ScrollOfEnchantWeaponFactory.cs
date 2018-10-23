using System;
using SomeName.Core.Balance;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class ScrollOfEnchantWeaponFactory : WeaponFactory
    {
        public override long GetItemGoldValue(int level)
            => Convert.ToInt64(GetBaseWeaponGoldValue(GetItemLevel(level)) * 0.30);

        public override Item Build(int level)
        {
            var newLevel = GetItemLevel(level);
            var item = new ScrollOfEnchantWeapon()
            {
                Level = newLevel,
            };
            item.GoldValue.Base = GetItemGoldValue(newLevel);
            item.UpdateGoldValueKoef();

            return item;
        }

        protected override int GetItemLevel(int level)
            => PlayerStatsBalance.MaxLevel;
    }
}
