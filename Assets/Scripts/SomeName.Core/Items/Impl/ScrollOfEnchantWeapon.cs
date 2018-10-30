using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.Impl
{
    public class ScrollOfEnchantWeapon : ScrollOfEnchant
    {
        public ScrollOfEnchantWeapon()
            : base()
        {
            Description = "Свиток заточки оружия";
            ImageId = "ScrollOfEnchantWeapon";
            ItemType = ItemType.ScrollOfEnchantWeapon;
        }

        public override IItem Clone()
        {
            var item = new ScrollOfEnchantWeapon();
            base.CloneTo(item);
            return item;
        }
    }
}
