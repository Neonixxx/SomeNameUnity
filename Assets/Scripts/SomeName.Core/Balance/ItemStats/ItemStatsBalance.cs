using SomeName.Core.Domain;
using SomeName.Core.Items.Bonuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;
using static System.Math;

namespace SomeName.Core.Balance.ItemStats
{
    public abstract class ItemStatsBalance
    {
        protected virtual double PowerKoef => 0.0;

        protected virtual double VitalityKoef => 0.0;

        protected virtual double AccuracyKoef => 0.0;

        protected virtual double EvasionKoef => 0.0;

        protected virtual double CritChanceKoef => 0.0;

        protected virtual double CritDamageKoef => 0.0;

        protected virtual double HealthPerSecondKoef => 0.0;

        public abstract ItemBonusesEnum[] PossibleItemBonuses { get; }

        // TODO : Сделать формулу.
        public virtual int GetMinItemBonusesCount(int level)
            => 1;

        // TODO : Сделать формулу.
        public virtual int GetMaxItemBonusesCount(int level)
            => 3;

        public double GetItemBonusesCountKoef(int level)
            => ToDouble((GetMaxItemBonusesCount(level) - GetMinItemBonusesCount(level))) / PossibleItemBonuses.Length;


        public BaseKoefValue<int> GetPower(int level, double damageValueKoef)
            => new BaseKoefValue<int> { Base = GetBasePower(level), Koef = damageValueKoef };

        private int GetBasePower(int level)
            => ToInt32(GetBaseStat(level) * PowerKoef);


        public BaseKoefValue<int> GetVitality(int level, double damageValueKoef)
            => new BaseKoefValue<int> { Base = GetBaseVitality(level), Koef = damageValueKoef };

        private int GetBaseVitality(int level)
            => ToInt32(GetBaseStat(level) * VitalityKoef);

        // f(1) = 2, f(100) = 400.
        private int GetBaseStat(int level)
            => ToInt32(Pow(level, 1.3) + 2);


        public BaseKoefValue<int> GetAccuracy(int level, double damageValueKoef)
            => new BaseKoefValue<int> { Base = GetBaseAccuracy(level), Koef = damageValueKoef };

        private int GetBaseAccuracy(int level)
            => ToInt32(GetBaseAccuracyEvasion(level) * AccuracyKoef);


        public BaseKoefValue<int> GetEvasion(int level, double damageValueKoef)
            => new BaseKoefValue<int> { Base = GetBaseEvasion(level), Koef = damageValueKoef };

        private int GetBaseEvasion(int level)
            => ToInt32(GetBaseAccuracyEvasion(level) * EvasionKoef);

        // f(1) = 2, f(100) = 400.
        private int GetBaseAccuracyEvasion(int level)
            => ToInt32(Pow(level, 1.3) + 2);


        public BaseKoefValue<double> GetCritChance(int level, double damageValueKoef)
            => new BaseKoefValue<double> { Base = GetBaseCritChance(level), Koef = Math.Min(damageValueKoef, 5) };

        // f(1) = 0.00515, f(100) = 0.02.
        private double GetBaseCritChance(int level)
            => CritChanceKoef * (level * 0.00015 + 0.005);


        public BaseKoefValue<double> GetCritDamage(int level, double damageValueKoef)
            => new BaseKoefValue<double> { Base = GetBaseCritDamage(level), Koef = damageValueKoef };

        // f(1) = 0.0313, f(100) = 0.16.
        private double GetBaseCritDamage(int level)
            => CritDamageKoef * (level * 0.0013 + 0.03);


        public BaseKoefValue<long> GetHealthPerHit(int level, double damageValueKoef)
            => new BaseKoefValue<long> { Base = GetBaseHealthPerHit(level), Koef = damageValueKoef };

        private long GetBaseHealthPerHit(int level)
            => ToInt64(PlayerStatsBalance.Standard.GetDefaultMaxHealth(level) * HealthPerSecondKoef / 350);
    }
}
