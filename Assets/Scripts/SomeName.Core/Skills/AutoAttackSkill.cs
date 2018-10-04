namespace SomeName.Core.Skills
{
    public class AutoAttackSkill : PowerStrike
    {
        public AutoAttackSkill() 
        {
            Description = "Auto attack";
            ImageId = ""; // TODO : сделать иконку.
            CastingTime = 0;
            Cooldown = 0;
            RequiredMana = 0;
            DamageKoef = 1.0;
            BonusDamage = 0;
            AccuracyKoef = 1.0;
        }
    }
}
