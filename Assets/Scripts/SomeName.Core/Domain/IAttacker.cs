using System;
using SomeName.Core.Items.Impl;

namespace SomeName.Core.Domain
{
    public interface IAttacker
    {
        long GetDamage();

        int GetAccuracy();

        int GetEvasion();

        double GetCritChance();

        double GetCritDamage();

        void OnHitActivate();

        event EventHandler OnEvade;

        SoulShot GetSoulShot();
    }
}
