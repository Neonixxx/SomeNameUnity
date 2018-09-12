using SomeName.Core.Balance;
using SomeName.Core.Monsters.Interfaces;
using SomeName.Core.Items.ItemFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core.Monsters.Impl
{
    public class SimpleMonster : Monster
    {
        public SimpleMonster(int level)
        {
            DropFactory = DropService.Standard;
            MonsterStatsBalance = MonsterStatsBalance.Standard;
            Respawn(level); 
        }
    }
}
