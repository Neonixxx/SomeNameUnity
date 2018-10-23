using SomeName.Core.Items.Bonuses;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class SimpleGlovesFactory : GlovesFactory
    {
        public override long GetItemGoldValue(int level)
            => GetBaseGlovesGoldValue(GetLevel(level));

        public override Item Build(int level, double additionalKoef = 1.0)
        {
            var newLevel = GetLevel(level);

            var damageValueKoef = RollItemDamageKoef(additionalKoef);
            var item = new SimpleGloves()
            {
                Level = newLevel,
                Defence = GlovesStatsBalance.GetDefence(newLevel, damageValueKoef),
                Bonuses = ItemBonusesFactory.Build(GlovesStatsBalance, newLevel, additionalKoef)
            };
            item.GoldValue.Base = GetBaseGlovesGoldValue(newLevel);
            item.UpdateGoldValueKoef();

            return item;
        }
    }
}
