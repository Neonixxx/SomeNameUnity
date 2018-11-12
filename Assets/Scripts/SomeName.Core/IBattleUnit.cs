using SomeName.Core.Domain;
using SomeName.Core.Services;

namespace SomeName.Core
{
    public interface IBattleUnit : IAttacker, IAttackTarget
    {
        SkillService SkillService { get; }

        EffectService EffectService { get; }
    }
}
