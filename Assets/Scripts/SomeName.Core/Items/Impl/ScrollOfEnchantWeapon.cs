﻿using SomeName.Core.Items.Interfaces;
using UnityEngine;

namespace SomeName.Core.Items.Impl
{
    public class ScrollOfEnchantWeapon : ScrollOfEnchant
    {
        public ScrollOfEnchantWeapon()
        {
            Description = "Свиток заточки оружия";
            Image = Resources.Load<Sprite>("ScrollOfEnchantWeapon.jpg");
        }
    }
}
