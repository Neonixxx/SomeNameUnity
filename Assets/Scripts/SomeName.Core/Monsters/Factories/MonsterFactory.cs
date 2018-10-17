using SomeName.Core.Domain;
using SomeName.Core.Monsters.Interfaces;

namespace SomeName.Core.Monsters.Factories
{
    public abstract class MonsterFactory
    {
        /// <summary>
        /// Получить монстра, производимого этой фабрикой.
        /// </summary>
        /// <param name="level">Уровень монстра.</param>
        /// <param name="monsterType">Тип монстра.</param>
        public abstract Monster Build(Level level, MonsterType monsterType);
    }
}
