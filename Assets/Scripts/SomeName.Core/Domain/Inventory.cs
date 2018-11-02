using System.Collections.Generic;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Domain
{
    public class Inventory
    {
        public EquippedItems EquippedItems { get; set; } = new EquippedItems();

        public List<IItem> Bag { get; set; } = new List<IItem>();

        public List<IItem> Cube { get; set; } = new List<IItem>();

        public SoulShot SoulShot { get; set; }

        public long Gold { get; set; }
    }
}
