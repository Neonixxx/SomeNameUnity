using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.Impl
{
    public class ScrollOfEnchantArmor : ScrollOfEnchant
    {
        public ScrollOfEnchantArmor()
            : base()
        {
            Description = "Свиток заточки брони";
            ImageId = "ScrollOfEnchantArmor";
            ItemType = ItemType.ScrollOfEnchantArmor;
        }

        public override IItem Clone()
        {
            var item = new ScrollOfEnchantArmor();
            base.CloneTo(item);
            return item;
        }
    }
}
