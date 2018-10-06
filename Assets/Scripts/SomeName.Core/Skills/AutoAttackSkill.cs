namespace SomeName.Core.Skills
{
    public class AutoAttackSkill : PowerStrike
    {
        public AutoAttackSkill() 
        {
            Description = "Auto attack";
            ImageId = "AutoAttack";
            CastingTime = 0.1;
            Cooldown = 0.3;
            RequiredMana = 0;
            DamageKoef = 1.0;
            BonusDamage = 0;
            AccuracyKoef = 1.0;
        }
    }
}
