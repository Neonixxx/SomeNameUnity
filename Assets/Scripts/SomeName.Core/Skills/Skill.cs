using Newtonsoft.Json;
using SomeName.Core.Domain;
using SomeName.Core.Services;

namespace SomeName.Core.Skills
{
    public abstract class Skill : ISkill
    {
        public virtual void Initialize(SkillService skillService, IAttacker attacker)
        {
            SkillService = skillService;
            Attacker = attacker;
        }

        [JsonIgnore]
        public SkillService SkillService { get; set; }

        [JsonIgnore]
        public IAttacker Attacker { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public string ImageId { get; set; }

        public bool IsCasting { get; protected set; }

        public double CurrentCastingTime { get; protected set; }

        public double CurrentCooldown { get; protected set; }

        public double CastingTime { get; set; }
        public double Cooldown { get; set; }

        public virtual void StartCasting() { }

        public virtual void StopCasting() { }

        public virtual void Update(IAttackTarget attackTarget, double timeDelta) { }

        public virtual void StartBattle()
        {
            IsCasting = false;
            CurrentCastingTime = 0;
            CurrentCooldown = 0;
        }

        public virtual void EndBattle()
        {

        }
    }
}
