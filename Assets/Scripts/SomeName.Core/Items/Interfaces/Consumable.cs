namespace SomeName.Core.Items.Interfaces
{
    public abstract class Consumable : Item
    {
        public Consumable()
            : base()
        {
            MaxQuantity = 1000;
            ItemType = ItemType.Consumable;
        }
    }
}
