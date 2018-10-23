using SomeName.Core.Balance.ItemStats;
using SomeName.Core.Items.Bonuses;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public class SimpleSwordFactory : WeaponFactory
    {
        public override long GetItemGoldValue(int level)
            => GetBaseWeaponGoldValue(GetItemLevel(level));

        public override Item Build(int level)
        {
            var newLevel = GetItemLevel(level);

            var globalDamageValueKoef = RollGlobalItemDamageKoef(level);
            var item = new SimpleSword()
            {
                Level = newLevel,
                Damage = WeaponStatsBalance.GetDamage(newLevel, RollItemStatDamageKoef(level, globalDamageValueKoef)),
                Bonuses = ItemBonusesFactory.Build(WeaponStatsBalance, newLevel, globalDamageValueKoef)
            };
            item.GoldValue.Base = GetBaseWeaponGoldValue(newLevel);
            item.UpdateGoldValueKoef();

            return item;
        }
    }
}
