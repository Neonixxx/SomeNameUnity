using SomeName.Core.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SomeName.Core.Items.Impl
{
    public class BeginnerSword : Weapon
    {
        public BeginnerSword()
        {
            Level = 1;
            Description = "Меч ученика";
            Damage.Base = 5;
            Damage.Koef = 1.0;
            Bonuses = new Bonuses.ItemBonuses();
            ImageId = "BeginnerSword";
        }
    }
}
