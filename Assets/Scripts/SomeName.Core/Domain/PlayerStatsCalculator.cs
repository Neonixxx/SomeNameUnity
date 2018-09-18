using SomeName.Core.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;

namespace SomeName.Core.Domain
{
    public class PlayerStatsCalculator
    {
        private static readonly long StartDefence = 0;
        private static readonly long DefencePerLevel = 10;

        private static readonly int StartPower = 20;
        private static readonly int PowerPerLevel = 10;

        private static readonly int StartVitality = 20;
        private static readonly int VitalityPerLevel = 10;

        private static readonly double HitChanceKoef = 0.7;

        private static readonly int StartAccuracy = 20;
        private static readonly int AccuracyPerLevel = 10;

        private static readonly int StartEvasion = 20;
        private static readonly int EvasionPerLevel = 10;

        private static readonly double StartCritChance = 0.05;

        private static readonly double StartCritDamage = 1.5;

        private int GetMaxHealthPerVitality(int level)
            => level >= 10
                ? level
                : 10;

        public long GetBaseDefenceValue(int level)
            => ToInt64(Math.Pow(level, 2));


        public long CalculateDamage(Player player)
            => CalculateDamage(player.GetPower(), player.Inventory.EquippedItems.Weapon);

        public long CalculateDamage(int power, Weapon weapon)
            => CalculateDamage(power, weapon?.Damage.Value ?? 1);

        public long CalculateDamage(int power, long weaponDamage)
            => ToInt64((1 + ToDouble(power) / 100) * weaponDamage);


        public double CalculateDefenceKoef(Player player)
            => CalculateDefenceKoef(player.Level, CalculateDefence(player));

        public double CalculateDefenceKoef(int level, long defence)
            => ToDouble(defence) / (defence + GetBaseDefenceValue(level));


        public long CalculateDefence(Player player)
            => CalculateDefence(player.Level, player.Inventory.EquippedItems);

        public long CalculateDefence(int level, EquippedItems equippedItems)
            => CalculateDefence(level, equippedItems.GetDefence());

        public long CalculateDefence(int level, long itemsDefence)
            => StartDefence + DefencePerLevel * level + itemsDefence;


        public int CalculatePower(Player player)
            => CalculatePower(player.Level, player.Inventory.EquippedItems);

        public int CalculatePower(int level, EquippedItems equippedItems)
            => CalculatePower(level, equippedItems.GetPower());

        public int CalculatePower(int level, int itemsPower)
            => StartPower + level * PowerPerLevel + itemsPower;


        public long CalculateMaxHealth(Player player)
            => CalculateMaxHealth(player.Level, CalculateVitality(player));

        public long CalculateMaxHealth(int level, int playerVitality)
            => ToInt64(GetMaxHealthPerVitality(level)) * playerVitality;


        public int CalculateVitality(Player player)
            => CalculateVitality(player.Level, player.Inventory.EquippedItems);

        public int CalculateVitality(int level, EquippedItems equippedItems)
            => CalculateVitality(level, equippedItems.GetVitality());

        public int CalculateVitality(int level, int itemsVitality)
            => StartVitality + level * VitalityPerLevel + itemsVitality;


        public double CalculateHitChance(int accuracy, int evasion)
        {
            if (accuracy <= evasion)
                return HitChanceKoef * accuracy / evasion;
            else
                return 1 - (1 - HitChanceKoef) * evasion / accuracy;
        }


        public int CalculateAccuracy(Player player)
            => CalculateAccuracy(player.Level, player.Inventory.EquippedItems);

        public int CalculateAccuracy(int level, EquippedItems equippedItems)
            => CalculateAccuracy(level, equippedItems.GetAccuracy());

        public int CalculateAccuracy(int level, int itemsAccuracy)
            => StartAccuracy + level * AccuracyPerLevel + itemsAccuracy;


        public int CalculateEvasion(Player player)
            => CalculateEvasion(player.Level, player.Inventory.EquippedItems);

        public int CalculateEvasion(int level, EquippedItems equippedItems)
            => CalculateEvasion(level, equippedItems.GetEvasion());

        public int CalculateEvasion(int level, int itemsEvasion)
            => StartEvasion + level * EvasionPerLevel + itemsEvasion;


        public double CalculateCritChance(Player player)
            => CalculateCritChance(player.Inventory.EquippedItems);

        public double CalculateCritChance(EquippedItems equippedItems)
            => CalculateCritChance(equippedItems.GetCritChance());

        public double CalculateCritChance(double itemsCritChance)
            => StartCritChance + itemsCritChance;


        public double CalculateCritDamage(Player player)
            => CalculateCritDamage(player.Inventory.EquippedItems);

        public double CalculateCritDamage(EquippedItems equippedItems)
            => CalculateCritDamage(equippedItems.GetCritDamage());

        public double CalculateCritDamage(double itemsCritDamage)
            => StartCritDamage + itemsCritDamage;


        public long CalculateHealthPerHit(Player player)
            => CalculateHealthPerHit(player.Inventory.EquippedItems);

        public long CalculateHealthPerHit(EquippedItems equippedItems)
            => CalculateHealthPerHit(equippedItems.GetHealthPerHit());

        public long CalculateHealthPerHit(long itemsHealthPerHit)
            => itemsHealthPerHit;

        public static readonly PlayerStatsCalculator Standard = new PlayerStatsCalculator();
    }
}
