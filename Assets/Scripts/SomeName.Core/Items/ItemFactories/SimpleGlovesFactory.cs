using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Items.Bonuses;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class SimpleGlovesFactory : GlovesFactory
    {
        public override long GetItemGoldValue(int level)
            => GetBaseGlovesGoldValue(GetItemLevel(level));

        public override IItem Build(int level)
            => Build(level, 1).ElementAt(0);

        public override IEnumerable<IItem> Build(int level, int count)
        {
            var result = new IItem[count];
            var newLevel = GetItemLevel(level);
            var baseGoldValue = GetBaseGlovesGoldValue(newLevel);
            for (int i = 0; i < count; i++)
            {
                var globalDamageValueKoef = RollGlobalItemDamageKoef(level);
                var item = new SimpleGloves()
                {
                    Level = newLevel,
                    Defence = GlovesStatsBalance.GetDefence(newLevel, RollItemStatDamageKoef(level, globalDamageValueKoef)),
                    Bonuses = ItemBonusesFactory.Build(GlovesStatsBalance, newLevel, globalDamageValueKoef),
                };
                item.GoldValue.Base = baseGoldValue;
                item.UpdateGoldValueKoef();
                result[i] = item;
            }
            return result;
        }
    }
}
