using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.Impl
{
    public class SimpleSword : Weapon
    {
        public SimpleSword()
            : base()
        {
            Description = "Стальной меч";
            ImageId = "SimpleSword";
        }

        public override IItem Clone()
        {
            var item = new SimpleSword();
            base.CloneTo(item);
            return item;
        }
    }
}
