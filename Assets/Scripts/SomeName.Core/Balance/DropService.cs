using System;
using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Domain;
using SomeName.Core.Items.Interfaces;
using SomeName.Core.Items.ItemFactories;

namespace SomeName.Core.Balance
{
    public class DropService
    {
        public DropItem[] DropItems { get; }


        public DropService(params DropItem[] dropItems)
        {
            DropItems = dropItems;
        }

        public Drop Build(int level, DropValue dropValue)
        {
            return new Drop
            {
                Gold = CalculateGoldDrop(dropValue.Gold),
                Exp = dropValue.Exp,
                Items = CalculateItemsDrop(level, dropValue.Items)
            };
        }

        protected virtual long CalculateGoldDrop(long value)
        {
            var randomKoef = Dice.GetRange(0.5, 1.5);
            return Convert.ToInt64(value * randomKoef);
        }

        protected virtual long CalculateExpDrop(long value)
            => value;

        protected virtual List<IItem> CalculateItemsDrop(int level, long value)
        {
            var possibleDropItems = DropItems.Where(s => level >= s.MinLevel).ToArray();
            var itemDropValue = Convert.ToDouble(value) / possibleDropItems.Sum(s => s.DropKoef);
            var items = new List<IItem>();
            foreach (var dropItem in possibleDropItems)
            {
                var currentItemDropValue = itemDropValue * dropItem.DropKoef;
                var dropKoef = currentItemDropValue / dropItem.ItemFactory.GetItemGoldValue(level);
                var itemsCount = GetDroppedItemsCount(dropKoef);
                items.AddRange(dropItem.ItemFactory.Build(level, itemsCount));
            }
            return items;
        }

        private int GetDroppedItemsCount(double dropKoef)
        {
            var p = 1 / (dropKoef + 1);
            var l = Dice.Roll;
            var n = Math.Log(l, 1 - p);
            return (int)Math.Truncate(n);
        }

        public static readonly DropService Standard = new DropService
        (
            new DropItem(new SimpleSwordFactory(), 100),
            new DropItem(new SimpleChestFactory(), 100),
            new DropItem(new SimpleGlovesFactory(), 100),
            new DropItem(new SimpleHelmetFactory(), 100),
            new DropItem(new ScrollOfEnchantWeaponFactory(), 20, 80),
            new DropItem(new ScrollOfEnchantArmorFactory(), 20, 80)
        );
    }
}
