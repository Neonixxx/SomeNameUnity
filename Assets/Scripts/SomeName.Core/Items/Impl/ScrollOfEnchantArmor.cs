using SomeName.Core.Items.Interfaces;
using UnityEngine;

namespace SomeName.Core.Items.Impl
{
    public class ScrollOfEnchantArmor : ScrollOfEnchant
    {
        public ScrollOfEnchantArmor()
            : base()
        {
            Description = "Свиток заточки брони";
            ImageId = "ScrollOfEnchantArmor";
        }

        public override IItem Clone()
        {
            var item = new ScrollOfEnchantArmor();
            base.CloneTo(item);
            return item;
        }
    }
}
