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
        public abstract Item Build(int level);


        protected readonly ItemBonusesFactory ItemBonusesFactory = new ItemBonusesFactory();

        /// <summary>
        /// Получить базовое значение ценности предмета для заданного уровня.
        /// </summary>
        /// <param name="level"></param>
        protected static long GetBaseItemGoldValue(int level)
            => DropBalance.Standard.GetBaseItemValue(level);

        public static double RollItemStatDamageKoef(int level, double globalDamageValueKoef)
            => (Dice.Roll + 1) * globalDamageValueKoef;

        /// <summary>
        /// Получить случано сгенерированное значение коэффицента урона предмета.
        /// </summary>
        /// <returns></returns>
        public static double RollGlobalItemDamageKoef(int level)
            => GetGlobalItemDamageKoef(Dice.Roll, GetOverlevel(level));

        /// <summary>
        /// Получить значение коэффицента урона предмета по значению броска кубика.
        /// </summary>
        /// <param name="diceValue">Значение броска кубика (0..1]</param>
        /// <returns></returns>
        public static double GetGlobalItemDamageKoef(double diceValue, int overLevel = 0)
            => Math.Min(
                Math.Pow(diceValue, -(K + 0.003 * overLevel)) + 0.01 * overLevel
                , 2.5);

        protected static int GetOverlevel(int level)
            => level > PlayerStatsBalance.MaxLevel
                ? level - PlayerStatsBalance.MaxLevel
                : 0;

        public const double K = 0.15;

        protected virtual int GetItemLevel(int level)
            => Math.Min(level, PlayerStatsBalance.MaxLevel);
    }
}
