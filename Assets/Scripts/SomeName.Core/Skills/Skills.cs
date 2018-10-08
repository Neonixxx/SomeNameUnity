using System.Collections.Generic;

namespace SomeName.Core.Skills
{
    public class Skills
    {
        public ISkill DefaultSkill { get; set; } = new AutoAttackSkill();

        public List<ISkill> ActiveSkills { get; set; } = new List<ISkill>() { new PowerStrike() { CastingTime = 0.9, DamageKoef = 7, AccuracyKoef = 1.6, Cooldown = 8 } };

        public List<ISkill> TrigerringSkills { get; set; } = new List<ISkill>() { new CounterEvasion() { TriggerChance = 1.0, DamageKoef = 0.6 } };
    }
}
