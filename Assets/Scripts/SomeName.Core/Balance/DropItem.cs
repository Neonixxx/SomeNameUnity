using SomeName.Core.Items.ItemFactories;

namespace SomeName.Core.Balance
{
    public class DropItem
    {
        public DropItem(ItemFactory itemFactory, int dropKoef, int minLevel = 0)
        {
            ItemFactory = itemFactory;
            DropKoef = dropKoef;
            MinLevel = minLevel;
        }

        public ItemFactory ItemFactory { get; set; }

        public int DropKoef { get; set; }

        public int MinLevel { get; set; }
    }
}
