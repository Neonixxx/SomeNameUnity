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
            if (_cube.Count < _maxCubeItemsCount)
            {
                _cube.Add(item);
                InventoryService.Remove(item);
            }
        }

        public void Pull(IItem item)
        {
            if (_cube.Contains(item))
            {
                _cube.Remove(item);
                InventoryService.AddItem(item);
            }
        }

        public void Transform()
            => _cubeRecipies.FirstOrDefault(s => s.Validate())?.Transform();
    }
}
