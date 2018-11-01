using System;
using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Forge
{
    public class Cube : MonoBehaviour
    {
        public Inventory.Inventory Inventory;
        public Text ActiveSlotDescription;
        public Text ActivePageDescription;
        public GameObject DraggingInventorySlot;
        public List<GameObject> ItemSlots;

        private ResourceManager _resourceManager;
        private InventoryService InventoryService;
        private CubeService CubeService;

        private List<InventorySlot> _cubeItemSlots;
        private InventorySlot _activeSlot;

        // Use this for initialization
        void Start()
        {
            var player = FindObjectOfType<GameState>().Player;
            InventoryService = player.InventoryService;
            Inventory.InventoryService = InventoryService;
            CubeService = new CubeService(player);
            _resourceManager = FindObjectOfType<ResourceManager>();

            _cubeItemSlots = ItemSlots.Select(s => s.GetComponent<InventorySlot>()).ToList();
            EventsSubscribe();
        }

        private void EventsSubscribe()
        {
            Inventory.DoubleClick += (obj, e) => PutInCube((InventorySlot)obj, e);
            Inventory.ActiveSlotChanged += (obj, e) => _activeSlot = null;
            //Inventory.DragEnded += (obj, e) => OnDragEnded((InventorySlot)obj, e);
            foreach (var item in _cubeItemSlots)
            {
                item.FirstClick += (obj, e) => SetActiveSlot((InventorySlot)obj);
                item.DoubleClick += (obj, e) => PullFromCube((InventorySlot)obj);
            }
        }

        private void PutInCube(InventorySlot obj, EventArgs e)
        {
            CubeService.Put(InventoryService.Get(Inventory.GetIndexOfInventorySlot(obj)));
        }

        private void PullFromCube(InventorySlot obj)
        {
            CubeService.Pull(CubeService.Get(_cubeItemSlots.IndexOf(obj)));
        }

        public void Transform()
            => CubeService.Transform();

        //private void OnDragEnded(InventorySlot inventorySlot, Vector2 pointerPosition)
        //{
        //    var pointerPos = new Vector3(pointerPosition.x, pointerPosition.y);
        //    InventorySlot equippedItem = null;
        //    foreach (var item in _equippedItemSlots)
        //    {
        //        var rectTransform = item.Key.GetComponent<RectTransform>();
        //        var localPos = pointerPos - rectTransform.position;
        //        if (rectTransform.rect.Contains(localPos))
        //        {
        //            equippedItem = item.Key;
        //            break;
        //        }
        //    }

        //    if (equippedItem != null && InventoryService.CompareItemTypes(
        //        InventoryService.Get(
        //            Inventory.GetIndexOfInventorySlot(inventorySlot))
        //        , _equippedItemSlots[equippedItem]))
        //    {
        //        EquipItem(inventorySlot);
        //    }
        //}

        // Update is called once per frame
        void Update()
        {
            // Отрисовка экипированных предметов.
            for (int i = 0; i < CubeService.Count; i++)
            {
                var item = _cubeItemSlots[i];
                item.BackgroundSpriteIsActive(false);
                item.IsWithItem = true;
                item.SetMainSprite(
                    _resourceManager.GetSprite(
                        CubeService.Get(i).ImageId));
            }
            for (int i = CubeService.Count; i < _cubeItemSlots.Count; i++)
            {
                var item = _cubeItemSlots[i];
                item.BackgroundSpriteIsActive(false);
                item.IsWithItem = false;
                item.SetMainSprite(null);
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
                ActiveSlotDescription.text = CubeService.Get(_cubeItemSlots.IndexOf(_activeSlot))
                    .ToString();
            }
            else
                ActiveSlotDescription.text = string.Empty;
        }

        public void SetActiveSlot(InventorySlot inventorySlot)
        {
            _activeSlot = inventorySlot;
            Inventory.SetActiveSlot(null);
        }
    }
}