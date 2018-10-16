using System.ComponentModel;

namespace SomeName.Core.Monsters.Interfaces
{
    public enum MonsterType
    {
        [Description("Обычный монстр")]
        Normal = 0,

        [Description("Элитный монстр")]
        Elite = 1,

        [Description("Босс")]
        Boss = 2,
    }
}
