using SomeName.Core.Items.Bonuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

namespace SomeName.Core.Items.Interfaces
{
    public class Chest : Armor
    {
        public Chest()
            : base()
        {
            ItemTypes = ItemType.Chest;
        }

        protected void CloneTo(Chest item)
            => base.CloneTo(item);
    }
}
