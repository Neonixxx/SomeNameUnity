using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeName.Core.Balance.ItemStats;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public abstract class WeaponFactory : ItemFactory
    {
        protected readonly WeaponStatsBalance WeaponStatsBalance = new WeaponStatsBalance();

        protected const double WeaponGoldValueKoef = 0.5;

        protected long GetWeaponGoldValue(int level, double damageValueKoef)
            => Convert.ToInt64(GetBaseWeaponGoldValue(level) * damageValueKoef);

        protected long GetBaseWeaponGoldValue(int level)
            => Convert.ToInt64(GetBaseItemGoldValue(level) * WeaponGoldValueKoef);
    }
}
