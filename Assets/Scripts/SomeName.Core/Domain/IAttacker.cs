namespace SomeName.Core.Domain
{
    public interface IAttacker
    {
        long GetDamage();

        int GetAccuracy();

        double GetCritChance();

        double GetCritDamage();

        void OnHit();
    }
}
