using SomeName.Core.Domain;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core
{
    public class EnchantManager
    {
        protected static readonly double BaseEnchantChance = 1.0;

        protected static readonly double BaseEnchantmentValue = 0.08;

        protected static readonly double EnchantmentValueEnc = 0.02;

        protected static readonly double EnchantmentChanceKoef = 0.95;


        public bool TryEnchant(ICanBeEnchanted itemToEnchant, ScrollOfEnchant scrollOfEnchant)
        {
            var enchantResult = Dice.TryGetChance(GetEnchantChance(itemToEnchant, scrollOfEnchant));
            if (enchantResult)
            {
                SetEnchantmentLevel(itemToEnchant, itemToEnchant.EnchantmentLevel + 1);
                return true;
            }
            else
                return false;
        }

        public double GetEnchantChance(ICanBeEnchanted itemToEnchant, ScrollOfEnchant scrollOfEnchant)
        {
            var levelDiff = itemToEnchant.Level >= scrollOfEnchant.Level
                ? itemToEnchant.Level - scrollOfEnchant.Level
                : 0;
            var enchantChance = BaseEnchantChance * Math.Pow(EnchantmentChanceKoef, itemToEnchant.EnchantmentLevel + levelDiff);
            return enchantChance;
        }

        public void SetEnchantmentLevel(ICanBeEnchanted itemToEnchant, int newEnchantmentLevel)
        {
            itemToEnchant.EnchantmentLevel = newEnchantmentLevel;
            CalculateKoef(itemToEnchant);
        }

        protected void CalculateKoef(ICanBeEnchanted itemToEnchant)
        {
            var enchantmentDamageKoef = 1 + itemToEnchant.EnchantmentLevel * (BaseEnchantmentValue + EnchantmentValueEnc / 2 * (1 + itemToEnchant.EnchantmentLevel));
            itemToEnchant.MainStat.EnchantKoef = enchantmentDamageKoef;
            itemToEnchant.UpdateGoldValueKoef();
        }

        public static readonly EnchantManager Standard = new EnchantManager();
    }
}
