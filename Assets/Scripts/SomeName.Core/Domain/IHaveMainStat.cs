using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Domain
{
    public interface IHaveMainStat : IEquippment
    {
        MainStat<long> MainStat { get; set; }
    }
}
