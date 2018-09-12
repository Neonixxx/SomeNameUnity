using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeName.Core.Balance.ItemStats;
using SomeName.Core.Domain;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class ScrollOfEnchantWeaponFactory : WeaponFactory
    {
        public override long GetItemGoldValue(int level)
            => Convert.ToInt64(GetBaseWeaponGoldValue(level) * 0.30);

        public override Item Build(int level, double additionalKoef = 1.0)
        {
            var item = new ScrollOfEnchantWeapon()
            {
                Level = level,
            };
            item.GoldValue.Base = GetItemGoldValue(level);
            item.UpdateGoldValueKoef();

            return item;
        }
    }
}
