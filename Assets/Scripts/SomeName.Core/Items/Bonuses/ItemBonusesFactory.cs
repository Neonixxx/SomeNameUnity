using SomeName.Core.Balance.ItemStats;
using SomeName.Core.Items.ItemFactories;

namespace SomeName.Core.Items.Bonuses
{
    public class ItemBonusesFactory
    {
        public ItemBonuses Build(ItemStatsBalance itemStatsBalance, int level, double globalDamageValueKoef)
        {
            var minBonusesCount = itemStatsBalance.GetMinItemBonusesCount(level);
            var maxBonusesCount = itemStatsBalance.GetMaxItemBonusesCount(level);
            var bonusesCount = Dice.GetRange(minBonusesCount, maxBonusesCount);

            var itemBonusesBuilder = new ItemBonusesBuilder(itemStatsBalance, level);
            foreach (var itemBonuseEnum in itemStatsBalance.PossibleItemBonuses.TakeRandom(bonusesCount))
                itemBonusesBuilder.Calculate(itemBonuseEnum, ItemFactory.RollItemStatDamageKoef(level, globalDamageValueKoef));

            return itemBonusesBuilder.Build();
        }
    }
}
