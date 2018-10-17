using SomeName.Core.Monsters.Interfaces;

namespace SomeName.Core.Monsters.Impl
{
    public class SimpleMonster : Monster
    {
        public SimpleMonster()
        {
            IsDead = false;
            IsDropTaken = false;
            Description = "Simple monster";
        }
    }
}
