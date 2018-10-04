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
        public EquippedItems EquippedItems { get; set; } = new EquippedItems();

        public List<IItem> Bag { get; set; } = new List<IItem>();

        public List<IItem> Cube { get; set; } = new List<IItem>();

        public long Gold { get; set; }
    }
}
