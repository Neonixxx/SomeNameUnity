using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.Impl
{
    public class BeginnerSword : Weapon
    {
        public BeginnerSword()
            : base()
        {
            Level = 1;
            Description = "Меч ученика";
            Damage.Base = 5;
            Damage.Koef = 1.0;
            Bonuses = new Bonuses.ItemBonuses();
            ImageId = "BeginnerSword";
        }

        public override IItem Clone()
        {
            var item = new BeginnerSword();
            base.CloneTo(item);
            return item;
        }
    }
}
