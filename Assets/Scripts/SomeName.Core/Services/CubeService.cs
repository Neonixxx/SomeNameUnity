using System;
using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Domain;
using SomeName.Core.Forge.Cube;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Services
{
    public class CubeService
    {
        public InventoryService InventoryService { get; set; }

        private readonly BaseRecipe[] _cubeRecipies;

        private readonly List<IItem> _cube;
        private int _maxCubeItemsCount = 4;

        public CubeService(Player player)
        {
            InventoryService = player.InventoryService;
            if (InventoryService.Inventory.Cube == null)
                InventoryService.Inventory.Cube = new List<IItem>();
            _cube = InventoryService.Inventory.Cube;
            _cubeRecipies = new BaseRecipe[]
            {
                new EnchantRecipe(_cube)
            };
        }

        public int Count
            => _cube.Count;

        public IItem Get(int index)
            => _cube[index];

        public void Put(IItem item)
        {
            var stacks = _cube.Where(i => i.Description == item.Description && i.Quantity < i.MaxQuantity).ToArray();
            // Заполняем их.
            foreach (var stack in stacks)
            {
                var quantityToAdd = Math.Min(stack.MaxQuantity - stack.Quantity, item.Quantity);
                stack.Quantity += quantityToAdd;
                item.Quantity -= quantityToAdd;
                if (item.Quantity == 0)
                {
                    InventoryService.Remove(item);
                    return;
                }
            }
            if (_cube.Count >= _maxCubeItemsCount)
                return;
            // Создаем новые стеки.
            while (item.Quantity > 0)
            {
                var quantityToAdd = Math.Min(item.Quantity, item.MaxQuantity);
                var itemToAdd = item.Clone();
                itemToAdd.Quantity = quantityToAdd;
                _cube.Add(itemToAdd);
                item.Quantity -= quantityToAdd;
            }
            InventoryService.Remove(item);
        }

        public void Pull(IItem item, int quantity = 1)
        {
            if (_cube.Contains(item))
            {
                var quantityToRemove = Math.Min(item.Quantity, quantity);
                item.Quantity -= quantityToRemove;
                if (item.Quantity <= 0)
                    _cube.Remove(item);
                var itemToAdd = item.Clone();
                itemToAdd.Quantity = quantityToRemove;
                InventoryService.AddItem(itemToAdd);
            }
        }

        public void Transform()
            => _cubeRecipies.FirstOrDefault(s => s.Validate())?.Transform();
    }
}
