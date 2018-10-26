using System.Text;
using SomeName.Core.Domain;
using SomeName.Core.Items.Bonuses;

namespace SomeName.Core.Items.Interfaces
{
    public abstract class Equippment : Item, ICanBeEnchanted, IEquippment
    {
        public Equippment()
        {
            Quantity = 1;
            MaxQuantity = 1;
        }

        public ItemBonuses Bonuses { get; set; }

        public abstract MainStat<long> MainStat { get; set; }

        public override void UpdateGoldValueKoef()
        {
            var mainStatKoef = MainStat.Koef * MainStat.EnchantKoef;
            var bonusesKoef = Bonuses.GetValueKoef();
            GoldValue.Koef = mainStatKoef * bonusesKoef;
        }

        public int EnchantmentLevel { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();
            if (EnchantmentLevel > 0)
                result.Append($"+{EnchantmentLevel} ");
            result.Append(base.ToString());
            return result.ToString();
        }

        protected void CloneTo(Equippment item)
        {
            base.CloneTo(item);
            item.Bonuses = Bonuses.Clone();
            item.EnchantmentLevel = EnchantmentLevel;
        }
    }
}
