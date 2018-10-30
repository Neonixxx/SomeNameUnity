using System.Text;
using SomeName.Core.Domain;
using static System.Environment;

namespace SomeName.Core.Items.Interfaces
{
    public abstract class Armor : Equippment
    {
        public Armor()
            : base()
        {
            ItemType = ItemType.Armor;
        }

        public MainStat<long> Defence { get; set; } = new MainStat<long>();

        public override MainStat<long> MainStat { get { return Defence; } set { Defence = value; } }

        public override string ToString()
        {
            var result = new StringBuilder($"{base.ToString()}{NewLine}Defence: {Defence.Value}");
            var bonusesString = Bonuses.ToString();
            if (bonusesString != string.Empty)
                result.Append($"{NewLine}{bonusesString}");
            return result.ToString();
        }

        protected void CloneTo(Armor item)
        {
            base.CloneTo(item);
            item.Defence = (MainStat<long>)Defence.Clone();
        }
    }
}
