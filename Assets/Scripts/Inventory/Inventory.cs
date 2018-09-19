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

        private ResourceManager _resourceManager;
        private InventoryService InventoryService;

        private Dictionary<InventorySlot, ItemType> _equippedItemSlots;
        private List<InventorySlot> _inventorySlots;
        private InventorySlot _activeSlot;

        // Use this for initialization
        void Start()
        {
            InventoryService = FindObjectOfType<GameState>().Player.InventoryService;
            _resourceManager = FindObjectOfType<ResourceManager>();
            _inventorySlots = InventorySlotsObject.Select(s => s.GetComponent<InventorySlot>()).ToList();

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
            // Отрисовка предметов инвентаря.
            for (var i = 0; i < _inventorySlots.Count && i < InventoryService.Count; i++)
            {
                _inventorySlots[i].IsWithItem = true;
                _inventorySlots[i].BackgroundSpriteIsActive(false);
                _inventorySlots[i].SetMainSprite(_resourceManager.GetSprite(InventoryService.Get(i).ImageId));
            }
            for (var i = InventoryService.Count; i < _inventorySlots.Count; i++)
            {
                _inventorySlots[i].IsWithItem = false;
                _inventorySlots[i].BackgroundSpriteIsActive(false);
                _inventorySlots[i].SetMainSprite(null);
            }
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
                    : InventoryService.Get(_inventorySlots.IndexOf(_activeSlot)).ToString();
            }
            else
                ActiveSlotDescription.text = string.Empty;
        }

        public void SetActiveSlot(InventorySlot inventorySlot)
            => _activeSlot = inventorySlot;

        public void EquipItem(InventorySlot inventorySlot)
        {
            if (inventorySlot.IsWithItem)
                InventoryService.Equip(_inventorySlots.IndexOf(inventorySlot));
        }

        public void UnequipItem(InventorySlot inventorySlot)
            => InventoryService.Unequip(_equippedItemSlots[inventorySlot]);
    }
}