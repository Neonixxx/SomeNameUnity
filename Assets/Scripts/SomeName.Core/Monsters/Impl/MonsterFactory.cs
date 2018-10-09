using SomeName.Core.Domain;
using SomeName.Core.Monsters.Interfaces;

namespace SomeName.Core.Monsters.Impl
{
    public static class MonsterFactory
    {
        /// <summary>
        /// Не доделано.
        /// </summary>
        /// <param name="level">Уровень монстра.</param>
        /// <returns></returns>
        public static Monster GetRandomMonster(Level level)
        {
            return new SimpleMonster(level.Normal);
        }
    }
}
