using SomeName.Core.Domain;
using SomeName.Core.Items.Bonuses;
using SomeName.Core.Items.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

namespace SomeName.Core.Items.Interfaces
{
    public abstract class Weapon : Equippment
    {
        public Weapon()
            : base()
        {
            ItemTypes = ItemType.Weapon;
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
            item.Damage = Damage.Clone();
        }
    }
}
