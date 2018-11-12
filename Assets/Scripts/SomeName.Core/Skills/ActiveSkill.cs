namespace SomeName.Core.Skills
{
    public abstract class ActiveSkill : Skill
    {
        public override void StartCasting()
        {
            base.StartCasting();
            if (SkillService.IsCasting || CurrentCooldown > 0)
                return;
            CurrentCastingTime = 0;
            IsCasting = true;
            SkillService.IsCasting = true;
        }

        public override void StopCasting()
        {
            base.StopCasting();
            if (IsCasting)
            {
                SkillService.IsCasting = false;
                IsCasting = false;
                CurrentCooldown = Cooldown;
            }
        }

        public override void Update(IBattleUnit attackTarget, double timeDelta)
        {
            base.Update(attackTarget, timeDelta);
            if (IsCasting)
            {
                CurrentCastingTime += timeDelta;
                if (CurrentCastingTime >= CastingTime)
                {
                    DoSkill(attackTarget, timeDelta);
                    StopCasting();
                }
            }
            else
            {
                CurrentCooldown = CurrentCooldown > timeDelta
                    ? CurrentCooldown - timeDelta
                    : 0;
            }
        }

        protected abstract void DoSkill(IBattleUnit attackTarget, double timeDelta);
    }
}
