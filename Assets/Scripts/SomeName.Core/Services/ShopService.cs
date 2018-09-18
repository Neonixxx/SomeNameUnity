using SomeName.Core.Domain;
using SomeName.Core.Items.Interfaces;
using SomeName.Core.Items.ItemFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core
{
    public class ShopService
    {
        public Player Player { get; set; }

        public Tuple<ItemFactory, double>[] SellingItemFactories => new Tuple<ItemFactory, double>[]
        {
            Tuple.Create<ItemFactory, double>(new SimpleSwordFactory(), 0.4),
            Tuple.Create<ItemFactory, double>(new SimpleChestFactory(), 0.4),
            Tuple.Create<ItemFactory, double>(new SimpleGlovesFactory(), 0.4),
            Tuple.Create<ItemFactory, double>(new ScrollOfEnchantWeaponFactory(), 0.2),
            Tuple.Create<ItemFactory, double>(new ScrollOfEnchantArmorFactory(), 0.2)
        };

        private const int SellingItemsRounds = 2;

        private List<IItem> _sellingItems = new List<IItem>();

        public List<IItem> GetSellingItems()
            => _sellingItems;

        public ShopService(Player player)
        {
            Player = player;
        }

        public void RefreshSellingItems(int level)
        {
            _sellingItems.Clear();
            foreach (var itemFactory in SellingItemFactories)
                for (int i = 0; i < SellingItemsRounds; i++)
                    if (Dice.TryGetChance(itemFactory.Item2))
                        _sellingItems.Add(itemFactory.Item1.Build(level));
        }

        public void Buy(IItem item)
        {
            if (!CanBuy(item))
                throw new ArgumentException("Ошибка при покупке предмета.");

            Player.Inventory.Gold -= item.GoldValue.Value;
            _sellingItems.Remove(item);
            Player.TakeItem(item);
        }

        public bool CanBuy(IItem item)
        {
            if (_sellingItems.Contains(item) && Player.Inventory.Gold >= item.GoldValue.Value)
                return true;

            return false;
        }
    }
}
