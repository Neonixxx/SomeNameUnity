using SomeName.Core.Difficulties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core
{
    public class GameController
    {
        public readonly string[] BattleDifficulties = BattleDifficulty.GetStrings();

        public int GetCurrentDifficultyIndex()
            => BattleDifficulty.CurrentIndex;

        public void SetBattleDifficulty(int battleDifficultyIndex)
            => BattleDifficulty.SetBattleDifficulty(battleDifficultyIndex);
    }
}
