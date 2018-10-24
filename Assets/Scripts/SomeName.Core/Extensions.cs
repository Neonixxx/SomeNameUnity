using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SomeName.Core
{
    public static class Extensions
    {
        public static int ToInt32(this string word)
            => Int32.Parse(word);

        public static int ToInt32(this double value)
            => Convert.ToInt32(value);

        public static int ToInt32(this long value)
            => Convert.ToInt32(value);

        public static long ToInt64(this string word)
            => Int64.Parse(word);

        public static double ToDouble(this int value)
            => Convert.ToDouble(value);

        public static double ToDouble(this long value)
            => Convert.ToDouble(value);

        public static string ToPercentString(this double value, int chars = 2)
            => (value * 100).ToString($"F{chars}") + "%";

        public static bool ContainsOnly<T>(this IEnumerable<T> enumerable, params T[] items)
            => enumerable.Count() == enumerable.Intersect(items).Count();

        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> list, int count)
        {
            var result = new List<T>(list);
            var resultCount = result.Count;
            for (int i = 0; i < list.Count() - count; i++)
            {
                resultCount--;
                result.RemoveAt(Dice.GetRange(0, resultCount));
            }
            return result;
        }

        public static double OneIfZero(this double number)
            => number == 0
                ? 1.0
                : number;

        public static string GetDescription<TEnum>(this TEnum value)
        => value.GetType().GetField(value.ToString())
            .GetCustomAttribute<DescriptionAttribute>()
            ?.Description ?? "";
    }
}
