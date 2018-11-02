using System;
using System.Collections.Generic;
using SomeName.Core.Balance.ItemStats;

namespace SomeName.Core.Items.Bonuses
{
    public class ItemBonusesBuilder
    {
        public int Level { get; }

        private readonly ItemStatsBalance _itemStatsBalance;

        private readonly ItemBonuses _itemBonuses;
        
        public ItemBonusesBuilder(ItemStatsBalance itemStatsBalance, int level)
        {
            Level = level;
            _itemStatsBalance = itemStatsBalance;
            _itemBonuses = new ItemBonuses();
        }

        public ItemBonuses Build()
            => _itemBonuses;

        // TODO : Убрать все методы CalculateXxx в словарь.
        private static readonly Dictionary<ItemBonusesEnum, Func<ItemBonusesBuilder, double, ItemBonusesBuilder>> _itemBonusesCalculators
            = new Dictionary<ItemBonusesEnum, Func<ItemBonusesBuilder, double, ItemBonusesBuilder>>
            {
                { ItemBonusesEnum.Power, (builder, damageValueKoef) => builder.CalculatePower(damageValueKoef) },
                { ItemBonusesEnum.Vitality, (builder, damageValueKoef) => builder.CalculateVitality(damageValueKoef) },
                { ItemBonusesEnum.Accuracy, (builder, damageValueKoef) => builder.CalculateAccuracy(damageValueKoef) },
                { ItemBonusesEnum.Evasion, (builder, damageValueKoef) => builder.CalculateEvasion(damageValueKoef) },
                { ItemBonusesEnum.CritChance, (builder, damageValueKoef) => builder.CalculateCritChance(damageValueKoef) },
                { ItemBonusesEnum.CritDamage, (builder, damageValueKoef) => builder.CalculateCritDamage(damageValueKoef) },
                { ItemBonusesEnum.HealthPerHit, (builder, damageValueKoef) => builder.CalculateHealthPerHit(damageValueKoef) },
            };

        public ItemBonusesBuilder Calculate(ItemBonusesEnum itemBonusesEnum, double damageValueKoef)
            => _itemBonusesCalculators[itemBonusesEnum].Invoke(this, damageValueKoef);

        public ItemBonusesBuilder CalculatePower(double damageValueKoef)
        {
            _itemBonuses.Power = _itemStatsBalance.GetPower(Level, damageValueKoef);
            return this;
        }

        public ItemBonusesBuilder CalculateVitality(double damageValueKoef)
        {
            _itemBonuses.Vitality = _itemStatsBalance.GetVitality(Level, damageValueKoef);
            return this;
        }

        public ItemBonusesBuilder CalculateAccuracy(double damageValueKoef)
        {
            _itemBonuses.Accuracy = _itemStatsBalance.GetAccuracy(Level, damageValueKoef);
            return this;
        }

        public ItemBonusesBuilder CalculateEvasion(double damageValueKoef)
        {
            _itemBonuses.Evasion = _itemStatsBalance.GetEvasion(Level, damageValueKoef);
            return this;
        }

        public ItemBonusesBuilder CalculateCritChance(double damageValueKoef)
        {
            _itemBonuses.CritChance = _itemStatsBalance.GetCritChance(Level, damageValueKoef);
            return this;
        }

        public ItemBonusesBuilder CalculateCritDamage(double damageValueKoef)
        {
            _itemBonuses.CritDamage = _itemStatsBalance.GetCritDamage(Level, damageValueKoef);
            return this;
        }

        public ItemBonusesBuilder CalculateHealthPerHit(double damageValueKoef)
        {
            _itemBonuses.HealthPerHit = _itemStatsBalance.GetHealthPerHit(Level, damageValueKoef);
            return this;
        }
    }
}
