using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeName.Core.Balance;
using SomeName.Core.Items.Bonuses;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.ItemFactories
{
    public abstract class ItemFactory
    {
        /// <summary>
        /// Получить ценность предмета, производимого этой фабрикой.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public abstract long GetItemGoldValue(int level);

        /// <summary>
        /// Получить предмет, производимый этой фабрикой.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public abstract Item Build(int level, double additionalKoef = 1.0);


        protected readonly ItemBonusesFactory ItemBonusesFactory = new ItemBonusesFactory();

        /// <summary>
        /// Получить базовое значение ценности предмета для заданного уровня.
        /// </summary>
        /// <param name="level"></param>
        protected static long GetBaseItemGoldValue(int level)
            => DropBalance.Standard.GetBaseItemValue(level);

        /// <summary>
        /// Получить случано сгенерированное значение коэффицента урона предмета.
        /// </summary>
        /// <returns></returns>
        public static double RollItemDamageKoef(double additionalKoef = 1.0)
            => GetItemDamageKoef(Dice.Roll) * additionalKoef;

        /// <summary>
        /// Получить значение коэффицента урона предмета по значению броска кубика.
        /// </summary>
        /// <param name="diceValue">Значение броска кубика (0..1]</param>
        /// <returns></returns>
        public static double GetItemDamageKoef(double diceValue)
            => Math.Pow(diceValue, -K);

        public const double K = 0.35;

        public static double GetAverageTotalItemDamageKoef(int koefsCount, double totalPow)
            => Math.Pow(GetAverageItemDamageKoef(totalPow), -koefsCount);

        public static double GetAverageItemDamageKoef(double totalPow)
            => 1 - totalPow * K;
    }
}
