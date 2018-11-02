using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.Impl
{
    public class SimpleHelmet : Helmet
    {
        public SimpleHelmet()
            : base()
        {
            Description = "Шлем";
            ImageId = "SimpleHelmet";
        }

        public override IItem Clone()
        {
            var item = new SimpleHelmet();
            base.CloneTo(item);
            return item;
        }
    }
}
