using System.Collections.Generic;

namespace SomeName.Core.Skills
{
    public class Skills
    {
        public ISkill DefaultSkill { get; set; } = new AutoAttackSkill();

        public List<ISkill> ActiveSkills { get; set; } = new List<ISkill>();

        public List<ISkill> TrigerringSkills { get; set; } = new List<ISkill>();
    }
}
