using SomeName.Core.Domain;
using SomeName.Core.Services;

namespace SomeName.Core.Monsters
{
    public class MonsterSkillController : SkillService
    {
        public MonsterSkillController(IAttacker attacker, Skills.Skills skills)
            : base(attacker, skills) { }

        public override void Update(IAttackTarget attackTarget, double timeDelta)
        {
            base.Update(attackTarget, timeDelta);

            if (IsCasting)
                return;

            foreach (var skill in Skills.ActiveSkills)
                skill.StartCasting();

            Skills.DefaultSkill.StartCasting();
        }
    }
}
