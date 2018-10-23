using SomeName.Core.Items.Bonuses;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class SimpleGlovesFactory : GlovesFactory
    {
        public override long GetItemGoldValue(int level)
            => GetBaseGlovesGoldValue(GetItemLevel(level));

        public override Item Build(int level)
        {
            var newLevel = GetItemLevel(level);

            var globalDamageValueKoef = RollGlobalItemDamageKoef(level);
            var item = new SimpleGloves()
            {
                Level = newLevel,
                Defence = GlovesStatsBalance.GetDefence(newLevel, RollItemStatDamageKoef(level, globalDamageValueKoef)),
                Bonuses = ItemBonusesFactory.Build(GlovesStatsBalance, newLevel, globalDamageValueKoef)
            };
            item.GoldValue.Base = GetBaseGlovesGoldValue(newLevel);
            item.UpdateGoldValueKoef();

            return item;
        }
    }
}
