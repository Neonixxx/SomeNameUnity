using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Services;
using SomeName.Core.Skills;

namespace SomeName.Core.Monsters
{
    public class MonsterSkillController : SkillService
    {
        public MonsterSkillController(IBattleUnit attacker, Skills.Skills skills)
            : base(attacker, skills) { }

        public override void Update(IBattleUnit attackTarget, double timeDelta)
        {
            base.Update(attackTarget, timeDelta);

            if (IsCasting)
                return;

            GetSkillsToCast().TakeRandomOne().StartCasting();
        }

        private IEnumerable<ISkill> GetSkillsToCast()
            => Skills.ActiveSkills.Where(s => !s.IsCasting && s.CurrentCooldown == 0)
                .Union(new[] { Skills.DefaultSkill });
    }
}
