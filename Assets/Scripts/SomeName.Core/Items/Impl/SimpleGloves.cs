using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.Impl
{
    public class SimpleGloves : Gloves
    {
        public SimpleGloves()
            : base()
        {
            Description = "Кожанные перчатки";
            ImageId = "SimpleGloves";
        }

        public override IItem Clone()
        {
            var item = new SimpleGloves();
            base.CloneTo(item);
            return item;
        }
    }
}
