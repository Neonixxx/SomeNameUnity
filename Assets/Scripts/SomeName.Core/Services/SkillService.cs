using System;
using System.Linq;
using SomeName.Core.Skills;

namespace SomeName.Core.Services
{
    public class SkillService
    {
        public SkillService(IBattleUnit attacker, Skills.Skills skills)
        {
            Attacker = attacker;
            Skills = skills;
            Skills.DefaultSkill?.Initialize(this, Attacker);
            Skills.ActiveSkills.ForEach(s => s.Initialize(this, Attacker));
            Skills.TrigerringSkills.ForEach(s => s.Initialize(this, Attacker));
        }

        public IBattleUnit Attacker { get; set; }

        public Skills.Skills Skills { get; set; }

        public bool IsCasting { get; set; }

        public virtual void Update(IBattleUnit attackTarget, double timeDelta)
        {
            Skills.DefaultSkill.Update(attackTarget, timeDelta);
            foreach (var skill in Skills.ActiveSkills)
                skill.Update(attackTarget, timeDelta);
            foreach (var skill in Skills.TrigerringSkills)
                skill.Update(attackTarget, timeDelta);
        }
        
        public void StartBattle()
        {
            IsCasting = false;
            Skills.DefaultSkill.StartBattle();
            foreach (var skill in Skills.ActiveSkills)
                skill.StartBattle();
            foreach (var skill in Skills.TrigerringSkills)
                skill.StartBattle();
        }

        public void EndBattle()
        {
            IsCasting = false;
            Skills.DefaultSkill.EndBattle();
            foreach (var skill in Skills.ActiveSkills)
                skill.EndBattle();
            foreach (var skill in Skills.TrigerringSkills)
                skill.EndBattle();
        }

        /// <summary>
        /// Получить скилл, который кастуется в данный момент.
        /// </summary>
        /// <returns>Скилл, который кастуется в данный момент.</returns>
        public ISkill GetCastingSkill()
        {
            if (!IsCasting)
                throw new InvalidOperationException("Ни один скилл не кастуется!");

            if (Skills.DefaultSkill.IsCasting)
                return Skills.DefaultSkill;

            return Skills.ActiveSkills.First(s => s.IsCasting);
        }
    }
}
