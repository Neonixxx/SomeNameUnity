using System.Collections.Generic;

namespace SomeName.Core.Skills
{
    public class Skills
    {
        public ISkill DefaultSkill { get; set; } = new AutoAttackSkill();

        public List<ISkill> ActiveSkills { get; set; } = new List<ISkill>() { new PowerStrike() { CastingTime = 1.2, DamageKoef = 14, AccuracyKoef = 1.6, Cooldown = 8 } };
    }
}
