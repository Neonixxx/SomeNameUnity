namespace SomeName.Core.Domain
{
    public class BaseKoefValue<T> where T : struct
    {
        public T Base { get; set; }

        public double Koef { get; set; }

        public virtual T Value => (T)((dynamic)Base * Koef);
    }
}
