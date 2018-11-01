using SomeName.Core.Domain;
using SomeName.Core.Items.Bonuses;
using static System.Convert;
using static System.Math;

namespace SomeName.Core.Balance.ItemStats
{
    public class WeaponStatsBalance : ItemStatsBalance
    {
        public override ItemBonusesEnum[] PossibleItemBonuses => new ItemBonusesEnum[] 
        {
            ItemBonusesEnum.Power,
            ItemBonusesEnum.Vitality,
            ItemBonusesEnum.Accuracy,
            ItemBonusesEnum.HealthPerHit,
            ItemBonusesEnum.CritDamage,
        };

        protected override double PowerKoef => 1.0;

        protected override double VitalityKoef => 1.0;

        protected override double AccuracyKoef => 1.0;

        protected override double HealthPerSecondKoef => 1.0;

        protected override double CritDamageKoef => 1.0 ;


        public MainStat<long> GetDamage(int level, double damageValueKoef)
            => new MainStat<long> { Base = GetBaseDamage(level), Koef = damageValueKoef, EnchantKoef = 1.0 };

        private long GetBaseDamage(int level)
            => ToInt64(20 * Pow(E, 0.04 * level) - 15);
    }
}
