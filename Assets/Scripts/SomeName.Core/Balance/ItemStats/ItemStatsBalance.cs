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

        private int GetBaseStat(int level)
            => ToInt32(Pow(level, 1.3));


        public BaseKoefValue<int> GetAccuracy(int level, double damageValueKoef)
            => new BaseKoefValue<int> { Base = GetBaseAccuracy(level), Koef = damageValueKoef };

        private int GetBaseAccuracy(int level)
            => ToInt32(GetBaseAccuracyEvasion(level) * AccuracyKoef);


        public BaseKoefValue<int> GetEvasion(int level, double damageValueKoef)
            => new BaseKoefValue<int> { Base = GetBaseEvasion(level), Koef = damageValueKoef };

        private int GetBaseEvasion(int level)
            => ToInt32(GetBaseAccuracyEvasion(level) * EvasionKoef);

        private int GetBaseAccuracyEvasion(int level)
            => ToInt32(Pow(level, 1.4));


        public BaseKoefValue<double> GetCritChance(int level, double damageValueKoef)
        {
            var baseCritCoef = Sqrt(damageValueKoef);
            var critKoef = baseCritCoef > 2.0
                ? 2.0
                : baseCritCoef;

            return new BaseKoefValue<double> { Base = GetBaseCritChance(level), Koef = critKoef };
        }

        private double GetBaseCritChance(int level)
            => CritChanceKoef * level / 2500 + 0.01;


        public BaseKoefValue<double> GetCritDamage(int level, double damageValueKoef)
            => new BaseKoefValue<double> { Base = GetBaseCritDamage(level), Koef = damageValueKoef };

        private double GetBaseCritDamage(int level)
            => CritDamageKoef * level / 300 + 0.05;


        public BaseKoefValue<long> GetHealthPerHit(int level, double damageValueKoef)
            => new BaseKoefValue<long> { Base = GetBaseHealthPerHit(level), Koef = damageValueKoef };

        private long GetBaseHealthPerHit(int level)
            => ToInt64(PlayerStatsBalance.Standard.GetDefaultMaxHealth(level) * HealthPerSecondKoef / 350);
    }
}
