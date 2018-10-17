using System.ComponentModel;

namespace SomeName.Core.Monsters.Interfaces
{
    public enum MonsterType
    {
        [Description("Обычный")]
        Normal = 0,

        [Description("Элитный")]
        Elite = 1,

        [Description("Босс")]
        Boss = 2,
    }
}
