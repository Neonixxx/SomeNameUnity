using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SomeName.Core;
using SomeName.Core.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        public List<GameObject> InventorySlotsObject = new List<GameObject>();
        public Text ActiveSlotDescription;
        public Text ActivePageDescription;

        private ResourceManager _resourceManager;
        private InventoryService InventoryService;

        private Dictionary<InventorySlot, ItemType> _equippedItemSlots;
        private List<InventorySlot> _inventorySlots;
        private InventorySlot _activeSlot;
        private int _currentPage = 1;
        private int _maxPage = 1;
        private int _itemsPerPage;

        // Use this for initialization
        void Start()
        {
            InventoryService = FindObjectOfType<GameState>().Player.InventoryService;
            _resourceManager = FindObjectOfType<ResourceManager>();
            _inventorySlots = InventorySlotsObject.Select(s => s.GetComponent<InventorySlot>()).ToList();
            _itemsPerPage = _inventorySlots.Count;

            var weaponSlot = GameObject.Find("WeaponSlot").GetComponent<InventorySlot>();
            var glovesSlot = GameObject.Find("GlovesSlot").GetComponent<InventorySlot>();
            var chestSlot = GameObject.Find("ChestSlot").GetComponent<InventorySlot>();
            _equippedItemSlots = new Dictionary<InventorySlot, ItemType>
            {
                { weaponSlot, ItemType.Weapon },
                { glovesSlot, ItemType.Gloves },
                { chestSlot, ItemType.Chest }
            };
            EventsSubscribe();
        }

        private void EventsSubscribe()
        {
            foreach (var item in _inventorySlots)
            {
                item.FirstClick += (obj, e) => SetActiveSlot((InventorySlot)obj);
                item.DoubleClick += (obj, e) => EquipItem((InventorySlot)obj);
            }
            foreach (var item in _equippedItemSlots)
            {
                item.Key.FirstClick += (obj, e) => SetActiveSlot((InventorySlot)obj);
                item.Key.DoubleClick += (obj, e) => UnequipItem((InventorySlot)obj);
            }
        }

        // Update is called once per frame
        void Update()
        {
            InventoryBagUpdate();
            // Отрисовка экипированных предметов.
            foreach (var item in _equippedItemSlots)
            {
                item.Key.BackgroundSpriteIsActive(false);
                if (InventoryService.IsEquipped(item.Value))
                {
                    item.Key.IsWithItem = true;
                    item.Key.SetMainSprite(
                        _resourceManager.GetSprite(
                            InventoryService.GetEquipped(item.Value).ImageId));
                }
                else
                {
                    item.Key.IsWithItem = false;
                    item.Key.SetMainSprite(null);
                }
            }
            ActiveSlotUpdate();
        }

        /// <summary>
        /// Отрисовка предметов инвентаря.
        /// </summary>
        private void InventoryBagUpdate()
        {
            var itemsCount = InventoryService.Count;

            _maxPage = itemsCount % _itemsPerPage == 0 && itemsCount != 0
                ? itemsCount / _itemsPerPage
                : itemsCount / _itemsPerPage + 1;
            if (_maxPage < _currentPage)
                _currentPage = _maxPage;

            var firstItemIndex = GetFirstItemIndex();
            var itemsOnPage = itemsCount <= _itemsPerPage * _currentPage
                ? itemsCount - firstItemIndex
                : _itemsPerPage;

            ActivePageDescription.text = $"{_currentPage} из {_maxPage}";

            for (var i = 0; i < itemsOnPage; i++)
            {
                _inventorySlots[i].IsWithItem = true;
                _inventorySlots[i].BackgroundSpriteIsActive(false);
                _inventorySlots[i].SetMainSprite(_resourceManager.GetSprite(InventoryService.Get(i + firstItemIndex).ImageId));
            }
            for (var i = itemsOnPage; i < _inventorySlots.Count; i++)
            {
                _inventorySlots[i].IsWithItem = false;
                _inventorySlots[i].BackgroundSpriteIsActive(false);
                _inventorySlots[i].SetMainSprite(null);
            }
        }

        private int GetFirstItemIndex()
            => _itemsPerPage * (_currentPage - 1);

        /// <summary>
        /// Отрисовка выбранного предмета.
        /// </summary>
        private void ActiveSlotUpdate()
        {
            if (_activeSlot == null)
                return;

            if (_activeSlot.IsWithItem)
            {
                _activeSlot.BackgroundSpriteIsActive(true);
                ActiveSlotDescription.text = _equippedItemSlots.Keys.Contains(_activeSlot)
                    ? InventoryService.GetEquipped(_equippedItemSlots[_activeSlot]).ToString()
                    : InventoryService.Get(_inventorySlots.IndexOf(_activeSlot) + GetFirstItemIndex()).ToString();
            }
            else
                ActiveSlotDescription.text = string.Empty;
        }

        public void SetActiveSlot(InventorySlot inventorySlot)
            => _activeSlot = inventorySlot;

        public void EquipItem(InventorySlot inventorySlot)
        {
            if (inventorySlot.IsWithItem)
                InventoryService.Equip(_inventorySlots.IndexOf(inventorySlot) + GetFirstItemIndex());
        }

        public void UnequipItem(InventorySlot inventorySlot)
            => InventoryService.Unequip(_equippedItemSlots[inventorySlot]);

        public void PreviousPage()
        {
            if (_currentPage > 1)
                _currentPage--;
        }

        public void NextPage()
        {
            if (_currentPage < _maxPage)
                _currentPage++;
        }
    }
}