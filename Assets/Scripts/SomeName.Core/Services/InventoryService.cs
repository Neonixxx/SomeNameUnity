using System;
using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Domain;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Services
{
    public class InventoryService : IInventoryBag
    {
        public Domain.Inventory Inventory { get; }

        private InventoryList _bag;

        public InventoryService(Domain.Inventory inventory)
        {
            Inventory = inventory;
            _bag = new InventoryList(Inventory.Bag);
        }

        public IItem this[int index]
            => _bag[index];

        public int Count => _bag.Count;

        public IItem GetEquipped(ItemType itemType)
        {
            switch(itemType)
            {
                case ItemType.Weapon: return Inventory.EquippedItems.Weapon;
                case ItemType.Gloves: return Inventory.EquippedItems.Gloves;
                case ItemType.Chest: return Inventory.EquippedItems.Chest;
                case ItemType.Helmet: return Inventory.EquippedItems.Helmet;
            }
            throw new ArgumentException();
        }

        public IItem Get(int index)
            => _bag.Get(index);

        public void Add(Drop drop)
        {
            Add(drop.Gold);
            AddRange(drop.Items);
        }

        /// <summary>
        /// Добавить золото в инвентарь.
        /// </summary>
        /// <param name="value">Количество добавляемого золота.</param>
        public void Add(long value)
            => Inventory.Gold += value;

        public void Add(IItem item)
        {
            if (item as SoulShot != null)
            {
                if (Inventory.SoulShot == null)
                    Inventory.SoulShot = (SoulShot)item;
                else
                {
                    Inventory.SoulShot.Quantity += item.Quantity;
                    item.Quantity = 0;
                }
                return;
            }
            _bag.Add(item);
        }

        public void AddRange(IEnumerable<IItem> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public void Remove(int itemIndex, int quantity = 1)
            => Remove(Get(itemIndex), quantity);

        public void Remove(IItem item, int quantity = 1)
        {
            if (BagContains(item))
                _bag.Remove(item, quantity);
            else if (item == Inventory.SoulShot)
                item.Quantity -= Math.Min(item.Quantity, quantity);
            else if (IsEquipped(item))
            {
                item.Quantity -= quantity;
                if (item.Quantity <= 0)
                    Inventory.EquippedItems.Remove(item);
            }
        }

        public bool BagContains(IItem item)
            => _bag.Contains(item);

        public bool IsEquipped(IItem item)
            => Inventory.EquippedItems.Any(i => i == item);

        public bool IsEquipped(ItemType itemType)
            => GetEquipped(itemType) != null;

        public bool Equip(int itemIndex)
            => Equip(Get(itemIndex));

        //TODO : Решить, как можно сделать метод более расширяемым к добавлению новых типов предметов.
        public bool Equip(IItem item)
        {
            if (item as Weapon != null)
            {
                if (Inventory.EquippedItems.Weapon != null)
                    Add(Inventory.EquippedItems.Weapon);
                var itemToEquip = item.Clone();
                Remove(item);
                Inventory.EquippedItems.Weapon = (Weapon)itemToEquip;
                return true;
            }
            if (item as Chest != null)
            {
                if (Inventory.EquippedItems.Chest != null)
                    Add(Inventory.EquippedItems.Chest);
                var itemToEquip = item.Clone();
                Remove(item);
                Inventory.EquippedItems.Chest = (Chest)itemToEquip;
                return true;
            }
            if (item as Gloves != null)
            {
                if (Inventory.EquippedItems.Gloves != null)
                    Add(Inventory.EquippedItems.Gloves);
                var itemToEquip = item.Clone();
                Remove(item);
                Inventory.EquippedItems.Gloves = (Gloves)itemToEquip;
                return true;
            }
            if (item as Helmet != null)
            {
                if (Inventory.EquippedItems.Helmet != null)
                    Add(Inventory.EquippedItems.Helmet);
                var itemToEquip = item.Clone();
                Remove(item);
                Inventory.EquippedItems.Helmet = (Helmet)itemToEquip;
                return true;
            }

            return false;
        }

        public void Unequip(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    if (Inventory.EquippedItems.Weapon != null)
                    {
                        Add(Inventory.EquippedItems.Weapon);
                        Inventory.EquippedItems.Weapon = null;
                    }
                    break;

                case ItemType.Chest:
                    if (Inventory.EquippedItems.Chest != null)
                    {
                        Add(Inventory.EquippedItems.Chest);
                        Inventory.EquippedItems.Chest = null;
                    }
                    break;

                case ItemType.Gloves:
                    if (Inventory.EquippedItems.Gloves != null)
                    {
                        Add(Inventory.EquippedItems.Gloves);
                        Inventory.EquippedItems.Gloves = null;
                    }
                    break;

                case ItemType.Helmet:
                    if (Inventory.EquippedItems.Helmet != null)
                    {
                        Add(Inventory.EquippedItems.Helmet);
                        Inventory.EquippedItems.Helmet = null;
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
