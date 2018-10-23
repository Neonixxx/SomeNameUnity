using SomeName.Core.Balance.ItemStats;
using SomeName.Core.Items.Bonuses;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class SimpleSwordFactory : WeaponFactory
    {
        public override long GetItemGoldValue(int level)
            => GetBaseWeaponGoldValue(GetLevel(level));

        public override Item Build(int level, double additionalKoef = 1.0)
        {
            var newLevel = GetLevel(level);

            var damageValueKoef = RollItemDamageKoef(additionalKoef);
            var item = new SimpleSword()
            {
                Level = newLevel,
                Damage = WeaponStatsBalance.GetDamage(newLevel, damageValueKoef),
                Bonuses = ItemBonusesFactory.Build(WeaponStatsBalance, newLevel, additionalKoef)
            };
            item.GoldValue.Base = GetBaseWeaponGoldValue(newLevel);
            item.UpdateGoldValueKoef();

            return item;
        }
    }
}
