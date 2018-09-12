using SomeName.Core.Monsters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
