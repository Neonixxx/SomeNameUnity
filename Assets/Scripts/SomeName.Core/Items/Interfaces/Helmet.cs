namespace SomeName.Core.Items.Interfaces
{
    public abstract class Helmet : Armor
    {
        public Helmet()
            : base()
        {
            ItemType = ItemType.Chest;
        }

        protected void CloneTo(Chest item)
            => base.CloneTo(item);
    }
}
