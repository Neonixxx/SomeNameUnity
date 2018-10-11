using SomeName.Core.Balance;
using SomeName.Core.Monsters.Interfaces;

namespace SomeName.Core.Monsters.Impl
{
    public class SimpleMonster : Monster
    {
        public SimpleMonster(int level)
        {
            DropFactory = DropService.Standard;
            MonsterStatsBalance = MonsterStatsBalance.Standard;
            Description = "Simple monster";
            Respawn(level); 
        }
    }
}
