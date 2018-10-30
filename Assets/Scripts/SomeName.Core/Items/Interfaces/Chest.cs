namespace SomeName.Core.Items.Interfaces
{
    public abstract class Chest : Armor
    {
        public Chest()
            : base()
        {
            ItemType = ItemType.Chest;
        }

        protected void CloneTo(Chest item)
            => base.CloneTo(item);
    }
}
