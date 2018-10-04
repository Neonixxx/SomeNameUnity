using SomeName.Core.Domain;

namespace SomeName.Core.Services
{
    public class SkillService
    {
        public SkillService(IAttacker attacker, Skills.Skills skills)
        {
            Attacker = attacker;
            Skills = skills;
            Skills.DefaultSkill?.Initialize(this, Attacker);
            Skills.ActiveSkills.ForEach(s => s.Initialize(this, Attacker));
        }

        public IAttacker Attacker { get; set; }

        public Skills.Skills Skills { get; set; }

        public bool IsCasting { get; set; }

        public void Update(IAttackTarget attackTarget, double timeDelta)
        {
            Skills.DefaultSkill.Update(attackTarget, timeDelta);
            foreach (var skill in Skills.ActiveSkills)
                skill.Update(attackTarget, timeDelta);
        }
        
        public void StartBattle()
        {
            IsCasting = false;
            Skills.DefaultSkill.StartBattle();
            foreach (var skill in Skills.ActiveSkills)
                skill.StartBattle();
        }
    }
}
