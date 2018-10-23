using SomeName.Core.Balance.ItemStats;
using SomeName.Core.Items.Bonuses;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class SimpleChestFactory : ChestFactory
    {
        public override long GetItemGoldValue(int level)
            => GetBaseChestGoldValue(GetLevel(level));

        public override Item Build(int level, double additionalKoef = 1.0)
        {
            var newLevel = GetLevel(level);

            var damageValueKoef = RollItemDamageKoef(additionalKoef);
            var item = new SimpleChest()
            {
                Level = newLevel,
                Defence = ChestStatsBalance.GetDefence(newLevel, damageValueKoef),
                Bonuses = ItemBonusesFactory.Build(ChestStatsBalance, newLevel, additionalKoef),
            };
            item.GoldValue.Base = GetBaseChestGoldValue(newLevel);
            item.UpdateGoldValueKoef();

            return item;
        }
    }
}
