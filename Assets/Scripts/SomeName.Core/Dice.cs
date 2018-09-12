using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core
{
    public static class Dice
    {
        private static readonly Random _rand = new Random();

        public static double Roll
            => _rand.NextDouble();

        public static bool TryGetChance(double chance)
            => chance == 0
            ? false
            : Roll <= chance;

        public static int GetRange(int from, int to)
            => _rand.Next(from, to + 1);

        public static double GetRange(double from, double to)
            => _rand.NextDouble() * (to - from) + from;
    }
}
