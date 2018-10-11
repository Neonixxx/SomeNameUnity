using SomeName.Core.Domain;
using SomeName.Core.Monsters.Impl;
using SomeName.Core.Monsters.Interfaces;

namespace SomeName.Core.Monsters.Factories
{
    public class SimpleMonsterFactory : MonsterFactory
    {
        public override Monster Build(Level level)
        {
            return new SimpleMonster(level.Normal);
        }
    }
}
