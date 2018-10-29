namespace SomeName.Core.Items.Interfaces
{
    public class Gloves : Armor
    {
        public Gloves()
            : base()
        {
            ItemTypes = ItemType.Gloves;
        }

        protected void CloneTo(Gloves item)
            => base.CloneTo(item);
    }
}
