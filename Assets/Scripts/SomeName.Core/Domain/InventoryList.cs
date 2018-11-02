using System;
using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Domain
{
    public class InventoryList
    {
        public InventoryList(List<IItem> items, int maxItems = 0)
        {
            Items = items;
            MaxItems = maxItems;
        }

        public List<IItem> Items { get; set; }

        public int MaxItems { get; set; }


        public int Count => Items.Count;

        public IItem this[int index] => Get(index);

        public IItem Get(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentException($"{nameof(index)} вне диапазона");

            return Items[index];
        }

        public void Add(IItem item)
            => Add(item, item.Quantity);

        public void Add(IItem item, int quantity)
        {
            // Возможно есть неполные стеки таких же предметов.
            var stacks = Items.Where(i => i.Description == item.Description && i.Quantity < i.MaxQuantity).ToArray();
            var startTotalQuantity = Math.Min(quantity, item.Quantity);
            var totalQuantity = startTotalQuantity;
            // Заполняем их.
            foreach (var stack in stacks)
            {
                var quantityToAdd = Math.Min(stack.MaxQuantity - stack.Quantity, totalQuantity);
                stack.Quantity += quantityToAdd;
                totalQuantity -= quantityToAdd;

                if (totalQuantity == 0)
                {
                    item.Quantity -= startTotalQuantity;
                    return;
                }
            }
            // Создаем новые стеки.
            while (totalQuantity > 0 && (MaxItems == 0 || Count < MaxItems))
            {
                var quantityToAdd = Math.Min(totalQuantity, item.MaxQuantity);
                var itemToAdd = item.Clone();
                itemToAdd.Quantity = quantityToAdd;
                Items.Add(itemToAdd);
                totalQuantity -= quantityToAdd;
            }
            item.Quantity -= startTotalQuantity;
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
            if (!Contains(item))
                throw new ArgumentException("Попытка удалить элемент, которого нет в списке.", nameof(item));

            item.Quantity -= quantity;
            if (item.Quantity <= 0)
                Items.Remove(item);
        }

        public bool Contains(IItem item)
            => Items.Contains(item);


        public List<IItem> ToList()
            => Items;

        public void Clear()
            => Items.Clear();
    }
}
