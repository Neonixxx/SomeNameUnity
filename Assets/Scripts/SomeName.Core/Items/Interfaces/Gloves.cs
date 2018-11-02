namespace SomeName.Core.Items.Interfaces
{
    public abstract class Gloves : Armor
    {
        public Gloves()
            : base()
        {
            ItemType = ItemType.Gloves;
        }

        protected void CloneTo(Gloves item)
            => base.CloneTo(item);
    }
}
