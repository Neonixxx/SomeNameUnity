using SomeName.Core.Domain;
using SomeName.Core.Items.Bonuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core.Items.Interfaces
{
    public abstract class Equippment : Item, ICanBeEnchanted, IEquippment
    {
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
    }
}
