using SomeName.Core.Difficulties;

namespace SomeName.Core.Services
{
    public class DifficultyService
    {
        public readonly string[] BattleDifficulties = BattleDifficulty.GetStrings();

        public int GetCurrentDifficultyIndex()
            => BattleDifficulty.CurrentIndex;

        public void SetBattleDifficulty(int battleDifficultyIndex)
            => BattleDifficulty.SetBattleDifficulty(battleDifficultyIndex);

        public static readonly DifficultyService Standard = new DifficultyService();
    }
}
