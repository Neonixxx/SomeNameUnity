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

        public override Item Build(int level)
        {
            var newLevel = GetItemLevel(level);

            var globalDamageValueKoef = RollGlobalItemDamageKoef(level);
            var item = new SimpleChest()
            {
                Level = newLevel,
                Defence = ChestStatsBalance.GetDefence(newLevel, RollItemStatDamageKoef(level, globalDamageValueKoef)),
                Bonuses = ItemBonusesFactory.Build(ChestStatsBalance, newLevel, globalDamageValueKoef),
            };
            item.GoldValue.Base = GetBaseChestGoldValue(newLevel);
            item.UpdateGoldValueKoef();

            return item;
        }
    }
}
