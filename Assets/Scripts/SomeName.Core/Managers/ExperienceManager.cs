using SomeName.Core.Balance;
using SomeName.Core.Domain;

namespace SomeName.Core.Managers
{
    public class ExperienceManager
    {
        public ExperienceManager(Player player)
        {
            Player = player;
        }

        public Player Player { get; set; }
        public DropBalance DropBalance { get; set; } = DropBalance.Standard;

        public const int MaxLevel = 100;

        public void TakeExp(long exp)
        {
            var totalExp = Player.Exp + exp;
            while (totalExp >= Player.ExpForNextLevel)
            {
                totalExp -= Player.ExpForNextLevel;
                if (Player.Level.Normal < MaxLevel)
                    Player.Level.Normal++;
                else
                    Player.Level.Paragon++;
                Player.ExpForNextLevel = DropBalance.GetExp(Player.Level);
            }
            Player.Exp = totalExp;
        }
    }
}
