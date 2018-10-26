using System;
using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Domain;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Services
{
    public class InventoryService : IInventoryBag
    {
        private readonly SomeName.Core.Domain.Inventory _inventory;

        public SomeName.Core.Domain.Inventory Inventory { get { return _inventory; } }

        public InventoryService(SomeName.Core.Domain.Inventory inventory)
        {
            _inventory = inventory;
        }


        public int Count => _inventory.Bag.Count;

        public IItem GetEquipped(ItemType itemType)
        {
            switch(itemType)
            {
                case ItemType.Weapon: return _inventory.EquippedItems.Weapon;
                case ItemType.Gloves: return _inventory.EquippedItems.Gloves;
                case ItemType.Chest: return _inventory.EquippedItems.Chest;
            }
            throw new ArgumentException();
        }

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
        {
            // Возможно есть неполные стеки таких же предметов.
            var stacks = Inventory.Where(i => i.Description == item.Description && i.Quantity < i.MaxQuantity).ToArray();
            // Заполняем их.
            foreach (var stack in stacks)
            {
                var quantityToAdd = Math.Min(stack.MaxQuantity - stack.Quantity, item.Quantity);
                stack.Quantity += quantityToAdd;
                item.Quantity += quantityToAdd;
                if (item.Quantity == 0)
                    return;
            }
            // Создаем новые стеки.
            while (item.Quantity > 0)
            {
                var quantityToAdd = Math.Min(item.Quantity, item.MaxQuantity);
                var itemToAdd = item.Clone();
                itemToAdd.Quantity = quantityToAdd;
                Inventory.Add(itemToAdd);
            }
        }

        public void AddItems(List<IItem> items)
        {
            foreach (var item in items)
                AddItem(item);
        }

        public void Remove(int itemIndex, int quantity = 1)
            => Remove(Get(itemIndex), quantity);

        public void Remove(IItem item, int quantity = 1)
        {
            if (BagContains(item))
            {
                item.Quantity -= quantity;
                if (item.Quantity <= 0)
                    _inventory.Bag.Remove(item);
            }
            else if (IsEquipped(item))
            {
                item.Quantity -= quantity;
                if (item.Quantity <= 0)
                    _inventory.EquippedItems.Remove(item);
            }
        }

        public bool BagContains(IItem item)
            => _inventory.Bag.Contains(item);

        public bool IsEquipped(IItem item)
            => _inventory.EquippedItems.Any(i => i == item);

        public bool IsEquipped(ItemType itemType)
            => GetEquipped(itemType) != null;

        public bool Equip(int itemIndex)
            => Equip(Get(itemIndex));

        //TODO : Решить, как можно сделать метод более расширяемым к добавлению новых типов предметов.
        public bool Equip(IItem item)
        {
            if (item as Weapon != null)
            {
                if (_inventory.EquippedItems.Weapon != null)
                    AddItem(_inventory.EquippedItems.Weapon);
                Remove(item);
                _inventory.EquippedItems.Weapon = (Weapon)item;
                return true;
            }
            if (item as Chest != null)
            {
                if (_inventory.EquippedItems.Chest != null)
                    AddItem(_inventory.EquippedItems.Chest);
                Remove(item);
                _inventory.EquippedItems.Chest = (Chest)item;
                return true;
            }
            if (item as Gloves != null)
            {
                if (_inventory.EquippedItems.Gloves != null)
                    AddItem(_inventory.EquippedItems.Gloves);
                Remove(item);
                _inventory.EquippedItems.Gloves = (Gloves)item;
                return true;
            }

            return false;
        }

        public void Unequip(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    if (_inventory.EquippedItems.Weapon != null)
                    {
                        AddItem(_inventory.EquippedItems.Weapon);
                        _inventory.EquippedItems.Weapon = null;
                    }
                    break;

                case ItemType.Chest:
                    if (_inventory.EquippedItems.Chest != null)
                    {
                        AddItem(_inventory.EquippedItems.Chest);
                        _inventory.EquippedItems.Chest = null;
                    }
                    break;

                case ItemType.Gloves:
                    if (_inventory.EquippedItems.Gloves != null)
                    {
                        AddItem(_inventory.EquippedItems.Gloves);
                        _inventory.EquippedItems.Gloves = null;
                    }
                    break;
            }
        }

        public bool CompareItemTypes(IItem item, ItemType itemType)
        {
            ItemType anotherItemType;
            if (item as Weapon != null)
                anotherItemType = ItemType.Weapon;
            else if (item as Chest != null)
                anotherItemType = ItemType.Chest;
            else if (item as Gloves != null)
                anotherItemType = ItemType.Gloves;
            else
                return false;

            if (anotherItemType == itemType)
                return true;
            else
                return false;
        }
    }
}
