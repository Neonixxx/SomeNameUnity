using SomeName.Core.Balance.ItemStats;
using SomeName.Core.Difficulties;
using SomeName.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;

namespace SomeName.Core.Balance
{
    public class PlayerStatsBalance
    {
        public PlayerStatsCalculator PlayerStatsCalculator { get; set; }

        public ItemStatsBalance[] ItemStatsBalances { get; set; }


        private double GetDefaultItemDamageKoef(int level)
            => BattleDifficulty.GetCurrent().ItemDamageKoef * GetBaseItemDamageKoef(level);

        private double GetBaseItemDamageKoef(int level)
            => 1 + level * 0.01;


        public static double GetTapsPerSecond(int level)
            => 2 + 0.03 * level;


        public long GetDefaultDPS(int level)
            => ToInt64(GetDefaultDamage(level) * GetTapsPerSecond(level));

        public long GetDefaultDamage(int level)
            => GetDamage(level, GetDefaultItemDamageKoef(level));

        public int GetDefaultAccuracy(int level)
            => GetAccuracy(level, GetDefaultItemDamageKoef(level));

        public int GetDefaultEvasion(int level)
            => GetEvasion(level, GetDefaultItemDamageKoef(level));

        public long GetDefaultToughness(int level)
            => ToInt64(GetDefaultMaxHealth(level) / (1 - GetDefaultDefenceKoef(level)));

        public long GetDefaultMaxHealth(int level)
            => GetMaxHealth(level, GetDefaultItemDamageKoef(level));

        public double GetDefaultDefenceKoef(int level)
        {
            var defence = GetDefence(level, GetDefaultItemDamageKoef(level));
            return PlayerStatsCalculator.CalculateDefenceKoef(level, defence);
        }

        public long GetDefaultTouchnessPerSecond(int level)
            => ToInt64(GetDefaultHealthPerSecond(level) / (1 - GetDefaultDefenceKoef(level)));

        public long GetDefaultHealthPerSecond(int level)
            => ToInt64(GetItemsHealthPerHit(level, GetDefaultItemDamageKoef(level)) * GetTapsPerSecond(level));

        private long GetDamage(int level, double damageValueKoef)
        {
            var power = GetPower(level, damageValueKoef);
            var weaponDamage = GetItemsDamage(level, damageValueKoef);

            var damageWithoutCritCoef = PlayerStatsCalculator.CalculateDamage(power, weaponDamage);
            var critCoef = GetBaseCritCoef(level, damageValueKoef);

            return ToInt64(damageWithoutCritCoef * critCoef);
        }

        private long GetItemsDamage(int level, double damageValueKoef)
            => ItemStatsBalances.Sum(i => (i as WeaponStatsBalance)?.GetDamage(level, damageValueKoef).Value ?? 0);


        private long GetDefence(int level, double damageValueKoef)
            => PlayerStatsCalculator.CalculateDefence(level, GetItemsDefence(level, damageValueKoef));

        private long GetItemsDefence(int level, double damageValueKoef)
            => ItemStatsBalances.Sum(i => (i as ArmorStatsBalance)?.GetDefence(level, damageValueKoef).Value ?? 0);


        private int GetPower(int level, double damageValueKoef)
            => PlayerStatsCalculator.CalculatePower(level, GetItemsPower(level, damageValueKoef));

        private int GetItemsPower(int level, double damageValueKoef)
            => ItemStatsBalances.Sum(i => ToInt32(i.GetPower(level, damageValueKoef).Value * i.GetItemBonusesCountKoef(level)));


        private long GetMaxHealth(int level, double damageValueKoef)
            => PlayerStatsCalculator.CalculateMaxHealth(level, GetVitality(level, damageValueKoef));

        private int GetVitality(int level, double damageValueKoef)
            => PlayerStatsCalculator.CalculateVitality(level, GetItemsVitality(level, damageValueKoef));

        private int GetItemsVitality(int level, double damageValueKoef)
            => ItemStatsBalances.Sum(i => ToInt32(i.GetVitality(level, damageValueKoef).Value * i.GetItemBonusesCountKoef(level)));


        private int GetAccuracy(int level, double damageValueKoef)
            => PlayerStatsCalculator.CalculateAccuracy(level, GetItemsAccuracy(level, damageValueKoef));

        private int GetItemsAccuracy(int level, double damageValueKoef)
            => ItemStatsBalances.Sum(i => ToInt32(i.GetAccuracy(level, damageValueKoef).Value * i.GetItemBonusesCountKoef(level)));


        private int GetEvasion(int level, double damageValueKoef)
            => PlayerStatsCalculator.CalculateEvasion(level, GetItemsEvasion(level, damageValueKoef));

        private int GetItemsEvasion(int level, double damageValueKoef)
            => ItemStatsBalances.Sum(i => ToInt32(i.GetEvasion(level, damageValueKoef).Value * i.GetItemBonusesCountKoef(level)));


        private double GetBaseCritCoef(int level, double damageValueKoef)
            => GetCritChance(level, damageValueKoef) * (GetCritDamage(level, damageValueKoef) - 1) + 1;


        private double GetCritChance(int level, double damageValueKoef)
            => PlayerStatsCalculator.CalculateCritChance(GetItemsCritChance(level, damageValueKoef));

        private double GetItemsCritChance(int level, double damageValueKoef)
            => ItemStatsBalances.Sum(i => i.GetCritChance(level, damageValueKoef).Value * i.GetItemBonusesCountKoef(level));


        private double GetCritDamage(int level, double damageValueKoef)
            => PlayerStatsCalculator.CalculateCritDamage(GetItemsCritDamage(level, damageValueKoef));

        private double GetItemsCritDamage(int level, double damageValueKoef)
            => ItemStatsBalances.Sum(i => i.GetCritDamage(level, damageValueKoef).Value * i.GetItemBonusesCountKoef(level));


        private long GetHealthPerHit(int level, double damageValueKoef)
            => PlayerStatsCalculator.CalculateHealthPerHit(GetItemsHealthPerHit(level, damageValueKoef));

        private long GetItemsHealthPerHit(int level, double damageValueKoef)
            => ItemStatsBalances.Sum(i => ToInt64(i.GetHealthPerHit(level, damageValueKoef).Value * i.GetItemBonusesCountKoef(level)));


        public static readonly PlayerStatsBalance Standard = new PlayerStatsBalance
        {
            PlayerStatsCalculator = PlayerStatsCalculator.Standard,
            ItemStatsBalances = new ItemStatsBalance[]
            {
                new WeaponStatsBalance(),
                new ChestStatsBalance(),
                new GlovesStatsBalance()
            }
        };
    }
}
