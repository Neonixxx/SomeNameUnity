using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.Impl
{
    public class SimpleChest : Chest
    {
        public SimpleChest()
            : base()
        {
            Description = "Кожанный жилет";
            ImageId = "SimpleChest";
        }

        public override IItem Clone()
        {
            var item = new SimpleChest();
            base.CloneTo(item);
            return item;
        }
    }
}
