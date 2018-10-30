using System.Text;
using SomeName.Core.Domain;
using static System.Environment;

namespace SomeName.Core.Items.Interfaces
{
    public abstract class Weapon : Equippment
    {
        public Weapon()
            : base()
        {
            ItemType = ItemType.Weapon;
        }

        public MainStat<long> Damage { get; set; } = new MainStat<long>();

        public override MainStat<long> MainStat { get { return Damage; } set { Damage = value; } }

        public override string ToString()
        {
            var result = new StringBuilder($"{base.ToString()}{NewLine}Damage: {Damage.Value}");
            var bonusesString = Bonuses.ToString();
            if (bonusesString != string.Empty)
                result.Append($"{NewLine}{bonusesString}");
            return result.ToString();
        }

        protected void CloneTo(Weapon item)
        {
            base.CloneTo(item);
            item.Damage = (MainStat<long>)Damage.Clone();
        }
    }
}
