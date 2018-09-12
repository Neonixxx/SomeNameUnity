namespace SomeName.Core.Domain
{
    public class MainStat<T> : BaseKoefValue<T> where T : struct
    {
        public double EnchantKoef { get; set; } = 1.0;

        public override T Value => (T)((dynamic)Base * Koef * EnchantKoef);
    }
}
