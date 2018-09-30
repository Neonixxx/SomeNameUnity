using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeName.Core.Domain;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Domain
{
    public class Inventory
    {
        public EquippedItems EquippedItems { get; set; }

        public List<IItem> Bag { get; set; }

        public List<IItem> Cube { get; set; } = new List<IItem>();

        public long Gold { get; set; }
    }
}
