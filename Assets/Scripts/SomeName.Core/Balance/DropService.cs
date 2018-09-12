using SomeName.Core.Difficulties;
using SomeName.Core.Domain;
using SomeName.Core.Items.ItemFactories;
using SomeName.Core.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core.Balance
{
    public class DropService
    {
        public Tuple<ItemFactory, int>[] ItemFactories { get; }


        public DropService(params Tuple<ItemFactory, int>[] itemFactories)
        {
            ItemFactories = itemFactories;
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
            var itemDropValue = Convert.ToDouble(value) / ItemFactories.Sum(s => s.Item2);
            var items = new List<IItem>();
            var itemAdditionalKoef = BattleDifficulty.GetCurrent().ItemAdditionalKoef;
            foreach (var itemFactory in ItemFactories)
            {
                var currentItemDropValue = itemDropValue * itemFactory.Item2;
                var dropChance = currentItemDropValue / itemFactory.Item1.GetItemGoldValue(level);
                if (Dice.TryGetChance(dropChance))
                    items.Add(itemFactory.Item1.Build(level, itemAdditionalKoef));
            }
            return items;
        }

        public static readonly DropService Standard = new DropService
        (
            Tuple.Create<ItemFactory, int>(new SimpleSwordFactory(), 100),
            Tuple.Create<ItemFactory, int>(new SimpleChestFactory(), 100),
            Tuple.Create<ItemFactory, int>(new SimpleGlovesFactory(), 100),
            Tuple.Create<ItemFactory, int>(new ScrollOfEnchantWeaponFactory(), 20),
            Tuple.Create<ItemFactory, int>(new ScrollOfEnchantArmorFactory(), 20)
        );
    }
}
