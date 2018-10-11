using System.Linq;
using SomeName.Core.Domain;
using SomeName.Core.Monsters.Factories;
using SomeName.Core.Monsters.Interfaces;

namespace SomeName.Core.Locations
{
    public class Location
    {
        public Location(int id, string name, int minLevel, int maxLevel, MonsterFactory[] monsterFactories)
        {
            Id = id;
            Name = name;
            MonstersMinLevel = minLevel;
            MonstersMaxLevel = maxLevel;
            _monsterFactories = monsterFactories;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int MonstersMinLevel { get; private set; }

        public int MonstersMaxLevel { get; private set; }

        private MonsterFactory[] _monsterFactories;

        public Monster GetMonster()
            => _monsterFactories.TakeRandom(1).ElementAt(0).Build(GetRandomLevel());

        private Level GetRandomLevel()
            => new Level { Normal = Dice.GetRange(MonstersMinLevel, MonstersMaxLevel) };

        public override string ToString()
            => $"{Name} (min {MonstersMinLevel}lvl)";

        private static MonsterFactory[] BaseMonsterFactories = new MonsterFactory[]
        {
            new SimpleMonsterFactory()
        };

        public static Location[] BaseLocations = new Location[]
        {
            new Location(1, "Нуболока", 1, 1, BaseMonsterFactories),
            new Location(2, "Окраины деревни говорящего острова", 2, 4, BaseMonsterFactories),
            new Location(3, "Лес говорящего острова", 5, 10, BaseMonsterFactories),
            new Location(4, "Руины говорящего острова", 10, 15, BaseMonsterFactories),
            new Location(5, "Руины Глудио", 15, 20, BaseMonsterFactories),
            new Location(6, "Окраины Диона", 20, 25, BaseMonsterFactories),
            new Location(7, "Чуть дальше от Диона", 25, 30, BaseMonsterFactories),
            new Location(8, "1", 30, 35, BaseMonsterFactories),
            new Location(9, "2", 35, 40, BaseMonsterFactories),
            new Location(10, "3", 40, 45, BaseMonsterFactories),
            new Location(11, "4", 45, 50, BaseMonsterFactories),
            new Location(12, "5", 50, 55, BaseMonsterFactories),
        };

        
    }
}
