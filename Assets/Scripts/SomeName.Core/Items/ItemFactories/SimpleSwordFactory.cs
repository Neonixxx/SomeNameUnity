using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeName.Core.Balance;
using SomeName.Core.Balance.ItemStats;
using SomeName.Core.Domain;
using SomeName.Core.Items.Bonuses;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class SimpleSwordFactory : WeaponFactory
    {
        public override long GetItemGoldValue(int level)
            => GetBaseWeaponGoldValue(level);

        public override Item Build(int level, double additionalKoef = 1.0)
        {
            var damageValueKoef = RollItemDamageKoef(additionalKoef);
            var item = new SimpleSword()
            {
                Level = level,
                Damage = WeaponStatsBalance.GetDamage(level, damageValueKoef),
                Bonuses = ItemBonusesFactory.Build(WeaponStatsBalance, level, additionalKoef)
            };
            item.GoldValue.Base = GetBaseWeaponGoldValue(level);
            item.UpdateGoldValueKoef();

            return item;
        }
    }
}
