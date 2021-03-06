﻿using SomeName.Core.Domain;
using SomeName.Core.Services;

namespace SomeName.Core.Skills
{
    public interface ISkill
    {
        bool IsCasting { get; }
        double CurrentCastingTime { get; }
        double CurrentCooldown { get; }
        double CastingTime { get; set; }
        double Cooldown { get; set; }

        string Description { get; set; }

        string ImageId { get; set; }

        void StartCasting();

        void StopCasting();

        void Update(IBattleUnit attackTarget, double timeDelta);

        void Initialize(SkillService skillService, IBattleUnit attacker);

        void StartBattle();

        void EndBattle();
    }
}
