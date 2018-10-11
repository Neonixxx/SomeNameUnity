using System.Linq;
using SomeName.Core.Domain;
using SomeName.Core.Monsters.Factories;
using SomeName.Core.Monsters.Interfaces;

namespace SomeName.Core.Locations
{
    public class Location
    {
        public Location(int minLevel, int maxLevel, MonsterFactory[] monsterFactories)
        {
            MonstersMinLevel = minLevel;
            MonstersMaxLevel = maxLevel;
            _monsterFactories = monsterFactories;
        }

        public int MonstersMinLevel { get; private set; }

        public int MonstersMaxLevel { get; private set; }

        private MonsterFactory[] _monsterFactories;

        public Monster GetMonster()
            => _monsterFactories.TakeRandom(1).ElementAt(0).Build(GetRandomLevel());

        private Level GetRandomLevel()
            => new Level { Normal = Dice.GetRange(MonstersMinLevel, MonstersMaxLevel) };
    }
}
