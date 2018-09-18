using System;
using System.Collections.Generic;
using SomeName.Core.Domain;
using SomeName.Core.Items.Interfaces;
using System.Linq;

namespace SomeName.Core.Services
{
    public class InventoryService
    {
        private readonly SomeName.Core.Domain.Inventory _inventory;

        public InventoryService(SomeName.Core.Domain.Inventory inventory)
        {
            _inventory = inventory;
        }


        public int Count => _inventory.Bag.Count;

        public IItem Get(int index)
        {

            if (index < 0 || index >= Count)
                throw new ArgumentException($"{nameof(index)} вне диапазона");

            return _inventory.Bag[index];

        }

        public void AddDrop(Drop drop)
        {
            AddGold(drop.Gold);
            AddItems(drop.Items);
        }

        public void AddGold(long value)
            => _inventory.Gold += value;

        public void AddItem(IItem item)
            => _inventory.Bag.Add(item);

        public void AddItems(List<IItem> items)
            => _inventory.Bag.AddRange(items);

        public void Remove(int itemIndex)
            => Remove(Get(itemIndex));

        public void Remove(IItem item)
        {
            if (BagContains(item))
                _inventory.Bag.Remove(item);
            else if (IsEquipped(item))
                _inventory.EquippedItems.Remove(item);
        }

        public void Swap(int itemIndex1, int itemIndex2)
        {
            throw new NotImplementedException();
        }

        public void Swap(IItem item, int itemIndex)
        {
            throw new NotImplementedException();
        }

        public void Swap(IItem item1, IItem item2)
        {
            throw new NotImplementedException();
        }

        public bool BagContains(IItem item)
            => _inventory.Bag.Contains(item);

        public bool IsEquipped(IItem item)
            => _inventory.EquippedItems.Any(i => i == item);
    }
}
