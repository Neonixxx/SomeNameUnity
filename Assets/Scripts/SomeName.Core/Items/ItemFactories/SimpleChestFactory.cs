using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Balance.ItemStats;
using SomeName.Core.Items.Bonuses;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class SimpleChestFactory : ChestFactory
    {
        public override long GetItemGoldValue(int level)
            => GetBaseChestGoldValue(GetItemLevel(level));

        public override IItem Build(int level)
            => Build(level, 1).ElementAt(0);

        public override IEnumerable<IItem> Build(int level, int count)
        {
            var result = new IItem[count];
            var newLevel = GetItemLevel(level);
            var baseGoldValue = GetBaseChestGoldValue(newLevel);
            for (int i = 0; i < count; i++)
            {
                var globalDamageValueKoef = RollGlobalItemDamageKoef(level);
                var item = new SimpleChest()
                {
                    Level = newLevel,
                    Defence = ChestStatsBalance.GetDefence(newLevel, RollItemStatDamageKoef(level, globalDamageValueKoef)),
                    Bonuses = ItemBonusesFactory.Build(ChestStatsBalance, newLevel, globalDamageValueKoef),
                };
                item.GoldValue.Base = baseGoldValue;
                item.UpdateGoldValueKoef();
                result[i] = item;
            }
            return result;
        }
    }
}
