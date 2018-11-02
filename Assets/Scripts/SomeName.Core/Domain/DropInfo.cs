using SomeName.Core.Monsters.Interfaces;
using static System.Environment;

namespace SomeName.Core.Domain
{
    public class DropInfo
    {
        public Monster KilledMonster { get; set; }

        public Drop Drop { get; set; }

        public DropInfo(Monster monster, Drop drop)
        {
            KilledMonster = monster;
            Drop = drop;
        }

        public override string ToString()
            => $"{KilledMonster.ToString()} убит" +
            $"{NewLine}Получена награда:" +
            $"{NewLine}{Drop.ToString()}";
    }
}
