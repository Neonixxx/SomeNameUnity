using System;
using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Balance;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class ScrollOfEnchantWeaponFactory : WeaponFactory
    {
        public override long GetItemGoldValue(int level)
            => Convert.ToInt64(GetBaseWeaponGoldValue(GetItemLevel(level)) * 0.30);

        public override IItem Build(int level)
            => Build(level, 1).ElementAt(0);

        public override IEnumerable<IItem> Build(int level, int count)
        {
            var newLevel = GetItemLevel(level);
            var item = new ScrollOfEnchantWeapon()
            {
                Level = newLevel,
                Quantity = count,
            };
            item.GoldValue.Base = GetItemGoldValue(newLevel);
            item.UpdateGoldValueKoef();

            return new[] { item };
        }

        protected override int GetItemLevel(int level)
            => PlayerStatsBalance.MaxLevel;
    }
}
