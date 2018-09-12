namespace SomeName.Core.Domain
{
    public interface ICanBeEnchanted : IHaveMainStat
    {
        int EnchantmentLevel { get; set; }
    }
}
