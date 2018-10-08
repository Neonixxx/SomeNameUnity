using System;

namespace SomeName.Core.Domain
{
    public interface IAttackTarget : ICanDie
    {
        long Health { get; set; }

        double GetDefenceKoef();

        int GetEvasion();

        void OnEvadeActivate(object obj, EventArgs e);
    }
}
