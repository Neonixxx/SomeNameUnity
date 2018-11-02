using SomeName.Core.Items.Bonuses;

namespace SomeName.Core.Balance.ItemStats
{
    public class HelmetStatsBalance : ArmorStatsBalance
    {
        public override ItemBonusesEnum[] PossibleItemBonuses => new ItemBonusesEnum[] 
        {
            ItemBonusesEnum.Power,
            ItemBonusesEnum.Vitality,
            ItemBonusesEnum.Evasion,
            ItemBonusesEnum.CritChance
        };

        protected override double DefenceKoef => 0.8;

        protected override double PowerKoef => 1.0;

        protected override double VitalityKoef => 1.0;

        protected override double EvasionKoef => 1.0;

        protected override double CritChanceKoef => 1.0;
    }
}
