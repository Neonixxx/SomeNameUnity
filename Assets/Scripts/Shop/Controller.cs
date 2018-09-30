using System.Collections;
using System.Collections.Generic;
using SomeName.Core.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class Controller : MonoBehaviour
    {
        public Inventory.Inventory Inventory;
        public Inventory.Inventory Shop;
        public Text GoldText;
        public Text ItemCostText;

        private InventoryService InventoryService;
        private ShopService ShopService;

        private RectTransform _canvasRect;
        private RectTransform _inventoryPanelRect;
        private RectTransform _shopPanelRect;

        // Use this for initialization
        void Start()
        {
            var player = FindObjectOfType<GameState>().Player;
            InventoryService = player.InventoryService;
            Inventory.InventoryService = InventoryService;
            ShopService = new ShopService(player);
            ShopService.RefreshSellingItems(player.Level);
            Shop.InventoryService = ShopService;
            _canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
            _inventoryPanelRect = Inventory.GetComponent<RectTransform>();
            _shopPanelRect = Shop.GetComponent<RectTransform>();
            EventsSubscribe();
        }

        private void EventsSubscribe()
        {
            Inventory.DoubleClick += (obj, e) => TrySell((InventorySlot)obj);
            Inventory.DragEnded += (obj, e) => OnInventoryDragEnded((InventorySlot)obj, e);

            Shop.DoubleClick += (obj, e) => TryBuy((InventorySlot)obj);
            Shop.DragEnded += (obj, e) => OnShopDragEnded((InventorySlot)obj, e);

            Inventory.ActiveSlotChanged += (obj, e) => SetInventoryActiveSlot((InventorySlot)obj);
            Shop.ActiveSlotChanged += (obj, e) => SetShopActiveSlot((InventorySlot)obj);
        }

        private void OnInventoryDragEnded(InventorySlot inventorySlot, Vector2 pointerPosition)
        {
            var pointerPos = new Vector3(pointerPosition.x, pointerPosition.y);
            var localPos = pointerPos - _shopPanelRect.position;
            if (_shopPanelRect.rect.Contains(localPos))
                TrySell(inventorySlot);
        }

        private void TrySell(InventorySlot inventorySlot)
        {
            var item = Inventory.GetItemOfInventorySlot(inventorySlot);
            if (ShopService.CanSell(item))
                ShopService.Sell(item);
        }

        private void OnShopDragEnded(InventorySlot inventorySlot, Vector2 pointerPosition)
        {
            var pointerPos = new Vector3(pointerPosition.x, pointerPosition.y);
            var localPos = pointerPos - _inventoryPanelRect.position;
            if (_shopPanelRect.rect.Contains(localPos))
                TryBuy(inventorySlot);
        }

        private void TryBuy(InventorySlot inventorySlot)
        {
            var item = Shop.GetItemOfInventorySlot(inventorySlot);
            if (ShopService.CanBuy(item))
                ShopService.Buy(item);
        }

        private void SetInventoryActiveSlot(InventorySlot inventorySlot)
        {
            var item = Inventory.GetItemOfInventorySlot(inventorySlot);
            ItemCostText.text = $"Продать за {ShopService.GetSellGoldCost(item).ToString()}";
            Shop.SetActiveSlot(null);
        }

        private void SetShopActiveSlot(InventorySlot inventorySlot)
        {
            var item = Shop.GetItemOfInventorySlot(inventorySlot);
            ItemCostText.text = $"Купить за {ShopService.GetBuyGoldCost(item).ToString()}";
            Inventory.SetActiveSlot(null);
        }

        private void Update()
        {
            GoldText.text = InventoryService.Inventory.Gold.ToString();
        }
    }
}