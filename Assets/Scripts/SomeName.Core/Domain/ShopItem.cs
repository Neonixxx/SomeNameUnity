using SomeName.Core.Items.ItemFactories;

namespace SomeName.Core.Domain
{
    public class ShopItem
    {
        public ShopItem(ItemFactory itemFactory, double sellChance, int minLevel = 0)
        {
            ItemFactory = itemFactory;
            SellChance = sellChance;
            MinLevel = minLevel;
        }

        public ItemFactory ItemFactory { get; set; }

        public double SellChance { get; set; }

        public int MinLevel { get; set; }
    }
}
