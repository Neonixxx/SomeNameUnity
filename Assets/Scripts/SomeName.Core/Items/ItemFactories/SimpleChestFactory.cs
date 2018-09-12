using SomeName.Core.Balance;
using SomeName.Core.Balance.ItemStats;
using SomeName.Core.Domain;
using SomeName.Core.Items.Bonuses;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core.Items.ItemFactories
{
    public class SimpleChestFactory : ChestFactory
    {
        public override long GetItemGoldValue(int level)
            => GetBaseChestGoldValue(level);

        public override Item Build(int level, double additionalKoef = 1.0)
        {
            var damageValueKoef = RollItemDamageKoef(additionalKoef);
            var item = new SimpleChest()
            {
                Level = level,
                Defence = ChestStatsBalance.GetDefence(level, damageValueKoef),
                Bonuses = ItemBonusesFactory.Build(ChestStatsBalance, level, additionalKoef),
            };
            item.GoldValue.Base = GetBaseChestGoldValue(level);
            item.UpdateGoldValueKoef();

            return item;
        }
    }
}
