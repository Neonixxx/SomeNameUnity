using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SomeName.Core;
using SomeName.Core.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class EquippedItems : MonoBehaviour
    {
        public Inventory Inventory;
        public Text ActiveSlotDescription;
        public Text ActivePageDescription;
        public GameObject DraggingInventorySlot;

        private ResourceManager _resourceManager;
        private InventoryService InventoryService;

        private Dictionary<InventorySlot, ItemType> _equippedItemSlots;
        private InventorySlot _activeSlot;

        // Use this for initialization
        void Start()
        {
            InventoryService = FindObjectOfType<GameState>().Player.InventoryService;
            _resourceManager = FindObjectOfType<ResourceManager>();

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
            Inventory.DragEnded += (obj, e) => OnDragEnded((InventorySlot)obj, e);
            foreach (var item in _equippedItemSlots)
            {
                item.Key.FirstClick += (obj, e) => SetActiveSlot((InventorySlot)obj);
                item.Key.DoubleClick += (obj, e) => UnequipItem((InventorySlot)obj);
            }
        }

        private void OnDragEnded(InventorySlot inventorySlot, Vector2 pointerPosition)
        {
            var pointerPos = new Vector3(pointerPosition.x, pointerPosition.y);
            InventorySlot equippedItem = null;
            foreach (var item in _equippedItemSlots)
            {
                var rectTransform = item.Key.GetComponent<RectTransform>();
                var localPos = pointerPos - rectTransform.position;
                if (rectTransform.rect.Contains(localPos))
                {
                    equippedItem = item.Key;
                    break;
                }
            }

            if (equippedItem != null && InventoryService.CompareItemTypes(
                InventoryService.Get(
                    Inventory.GetIndexOfInventorySlot(inventorySlot))
                , _equippedItemSlots[equippedItem]))
            {
                EquipItem(inventorySlot);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Отрисовка экипированных предметов.
            foreach (var item in _equippedItemSlots)//*
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
                ActiveSlotDescription.text = InventoryService.GetEquipped(_equippedItemSlots[_activeSlot]).ToString();
            }
            else
                ActiveSlotDescription.text = string.Empty;
        }

        public void SetActiveSlot(InventorySlot inventorySlot)
        {
            _activeSlot = inventorySlot;

            if (_activeSlot.IsWithItem)
            {
                _activeSlot.BackgroundSpriteIsActive(true);
                ActiveSlotDescription.text = InventoryService.GetEquipped(_equippedItemSlots[_activeSlot])
                    .ToString();
            }
            else
                ActiveSlotDescription.text = string.Empty;
        }

        public void EquipItem(InventorySlot inventorySlot)
        {
            if (inventorySlot.IsWithItem)
                InventoryService.Equip(Inventory.GetIndexOfInventorySlot(inventorySlot));
        }

        public void UnequipItem(InventorySlot inventorySlot)
            => InventoryService.Unequip(_equippedItemSlots[inventorySlot]);
    }
}