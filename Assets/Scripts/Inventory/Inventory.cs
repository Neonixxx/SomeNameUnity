﻿using System;
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
        public GameObject DraggingInventorySlot;

        private ResourceManager _resourceManager;
        private InventoryService InventoryService;

        private RectTransform _canvasRect;
        private RectTransform _draggingItemRect;
        private List<InventorySlot> _inventorySlots;
        private InventorySlot _activeSlot;
        private InventorySlot _draggingSlot;
        private int _currentPage = 1;
        private int _maxPage = 1;
        private int _itemsPerPage;
        private bool _isDragging;

        // Use this for initialization
        protected virtual void Start()
        {
            _canvasRect = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();
            _draggingItemRect = DraggingInventorySlot.GetComponent<RectTransform>();
            InventoryService = FindObjectOfType<GameState>().Player.InventoryService;
            _resourceManager = FindObjectOfType<ResourceManager>();
            _inventorySlots = InventorySlotsObject.Select(s => s.GetComponent<InventorySlot>()).ToList();
            _itemsPerPage = _inventorySlots.Count;

            EventsSubscribe();
        }

        private void EventsSubscribe()
        {
            foreach (var item in _inventorySlots)
            {
                item.FirstClick += (obj, e) => SetActiveSlot((InventorySlot)obj);
                item.DoubleClick += (obj, e) => DoubleClick?.Invoke(obj, e);
                item.DragStarted += (obj, e) => DragStarted((InventorySlot)obj, e);
                item.Drag += (obj, e) => Drag((InventorySlot)obj, e);
                item.DragEnded += (obj, e) => OnDragEnded((InventorySlot)obj, e);
            }
        }

        private void DragStarted(InventorySlot inventorySlot, Vector2 pointerPosition)
        {
            _isDragging = true;
            _draggingSlot = inventorySlot;
            DraggingInventorySlot.GetComponent<InventorySlot>().SetMainSprite(inventorySlot.MainSprite);
            DraggingInventorySlot.SetActive(true);
        }

        private void Drag(InventorySlot inventorySlot, Vector2 pointerPosition)
        {
            _draggingItemRect.localPosition = new Vector3(pointerPosition.x, pointerPosition.y) - _canvasRect.localPosition;
        }

        private void OnDragEnded(InventorySlot inventorySlot, Vector2 pointerPosition)
        {
            _isDragging = false;
            _draggingSlot = null;
            DraggingInventorySlot.GetComponent<InventorySlot>().SetMainSprite(null);
            DraggingInventorySlot.SetActive(false);
            DragEnded?.Invoke(inventorySlot, pointerPosition);
        }

        public event EventHandler DoubleClick;

        public event EventHandler<Vector2> DragEnded;

        // Update is called once per frame
        void Update()
        {
            InventoryBagUpdate();
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

        public int GetIndexOfInventorySlot(InventorySlot inventorySlot)
            => _inventorySlots.IndexOf(inventorySlot) + GetFirstItemIndex();

        private int GetFirstItemIndex()
            => _itemsPerPage * (_currentPage - 1);

        public void SetActiveSlot(InventorySlot inventorySlot)
        {
            _activeSlot = inventorySlot;

            if (_activeSlot.IsWithItem)
            {
                _activeSlot.BackgroundSpriteIsActive(true);
                ActiveSlotDescription.text = InventoryService.Get(GetIndexOfInventorySlot(_activeSlot))
                    .ToString();
            }
            else
                ActiveSlotDescription.text = string.Empty;
        }

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