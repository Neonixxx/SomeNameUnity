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
        private readonly InventoryList _cubeList;
        private int _maxCubeItemsCount = 4;

        public CubeService(Player player)
        {
            InventoryService = player.InventoryService;
            if (InventoryService.Inventory.Cube == null)
                InventoryService.Inventory.Cube = new List<IItem>();
            _cube = InventoryService.Inventory.Cube;
            _cubeList = new InventoryList(_cube, _maxCubeItemsCount);
            _cubeRecipies = new BaseRecipe[]
            {
                new EnchantRecipe(_cube)
            };
        }

        public int Count
            => _cubeList.Count;

        public IItem this[int index]
            => _cubeList[index];

        public IItem Get(int index)
            => _cubeList.Get(index);

        public void Put(IItem item, int quantity = 1)
        {
            _cubeList.Add(item, quantity);
            if (item.Quantity == 0)
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
                InventoryService.Add(itemToAdd);
            }
        }

        public void Transform()
            => _cubeRecipies.FirstOrDefault(s => s.Validate())?.Transform();
    }
}
