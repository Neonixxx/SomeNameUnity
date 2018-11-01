using System;
using System.Collections.Generic;
using SomeName.Core.Domain;
using SomeName.Core.Items.Interfaces;
using SomeName.Core.Items.ItemFactories;

namespace SomeName.Core.Services
{
    public class ShopService : IInventoryBag
    {
        public ShopService(Player player)
        {
            Player = player;
        }

        public Player Player { get; set; }

        public ShopItem[] ShopItems => new ShopItem[]
        {
            new ShopItem(new SimpleSwordFactory(), 0.4),
            new ShopItem(new SimpleChestFactory(), 0.4),
            new ShopItem(new SimpleGlovesFactory(), 0.4),
            new ShopItem(new ScrollOfEnchantWeaponFactory(), 0.2, 80),
            new ShopItem(new ScrollOfEnchantArmorFactory(), 0.2, 80)
        };

        private const int SellingItemsRounds = 2;
        private const double SellItemsKoef = 0.3;

        private InventoryList _sellingItems = new InventoryList(new List<IItem>());

        public int Count
            => _sellingItems.Count;

        public IItem this[int index]
            => _sellingItems[index];

        public IItem Get(int index)
            => _sellingItems.Get(index);

        public List<IItem> GetSellingItems()
            => _sellingItems.ToList();

        public void RefreshSellingItems(Level level)
        {
            _sellingItems.Clear();
            foreach (var shopItem in ShopItems)
            {
                if (level.Normal < shopItem.MinLevel)
                    continue;
                for (int i = 0; i < SellingItemsRounds; i++)
                    if (Dice.TryGetChance(shopItem.SellChance))
                        _sellingItems.Add(shopItem.ItemFactory.Build(level.Normal));
            }
        }

        public void Buy(IItem item, int quantity = 1)
        {
            if (!CanBuy(item))
                throw new ArgumentException("Ошибка при покупке предмета.");

            Player.Inventory.Gold -= GetBuyGoldCost(item, quantity);
            var itemToAdd = item.Clone();
            itemToAdd.Quantity = quantity;
            Player.InventoryService.Add(itemToAdd);

            _sellingItems.Remove(item, quantity);
        }

        public bool CanBuy(IItem item, int quantity = 1)
        {
            if (_sellingItems.Contains(item) && Player.Inventory.Gold >= GetBuyGoldCost(item, quantity))
                return true;

            return false;
        }

        public long GetMaxQuantityCanBuy(IItem item)
            => Player.Inventory.Gold / GetBuyGoldCost(item);

        public long GetBuyGoldCost(IItem item, int quantity = 1)
            => item.GoldValue.Value * quantity;

        public void Sell(IItem item, int quantity = 1)
        {
            if (!CanSell(item) || quantity > item.Quantity)
                throw new ArgumentException("Ошибка при продаже предмета.");

            Player.InventoryService.Remove(item, quantity);
            Player.Inventory.Gold += GetSellGoldCost(item, quantity);
            var soldItem = item.Clone();
            soldItem.Quantity = quantity;
            _sellingItems.Add(soldItem);
        }

        public bool CanSell(IItem item)
            => Player.Inventory.Bag.Contains(item);

        public long GetSellGoldCost(IItem item, int quantity = 1)
            => Convert.ToInt64(item.GoldValue.Value * SellItemsKoef * quantity);
    }
}
