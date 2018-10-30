namespace SomeName.Core.Items.Interfaces
{
    public abstract class ScrollOfEnchant : Item
    {
        public ScrollOfEnchant()
            : base()
        {
            MaxQuantity = 1000;
            ItemType = ItemType.ScrollOfEnchant;
        }
    }
}
