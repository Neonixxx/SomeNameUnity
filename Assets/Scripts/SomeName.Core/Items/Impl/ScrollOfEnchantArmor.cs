using SomeName.Core.Items.Interfaces;
using UnityEngine;

namespace SomeName.Core.Items.Impl
{
    public class ScrollOfEnchantArmor : ScrollOfEnchant
    {
        public ScrollOfEnchantArmor()
        {
            Description = "Свиток заточки брони";
            Image = Resources.Load<Sprite>("ScrollOfEnchantArmor.jpg");
        }
    }
}
