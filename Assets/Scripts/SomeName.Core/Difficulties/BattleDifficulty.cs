﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core.Difficulties
{
    public class BattleDifficulty
    {
        public string Name { get; private set; }

        public double ItemDamageKoef { get; private set; }

        public double ExpMultiplier { get; private set; }

        public double GoldMultiplier { get; private set; }

        public double DropMultiplier { get; private set; }

        public double ItemAdditionalKoef { get; private set; }

        private static readonly BattleDifficulty[] BattleDifficulties = new BattleDifficulty[]
        {
            new BattleDifficulty("Very easy", 0.65, 0.4, 0.4, 0.4, 0.8),
            new BattleDifficulty("Easy", 0.8, 0.6, 0.6, 0.6, 0.9),
            new BattleDifficulty("Normal", 1.0, 1.0, 1.0, 1.0, 1.0),
            new BattleDifficulty("Hard", 1.6, 2.0, 1.8, 1.8, 1.05),
            new BattleDifficulty("Super hard", 2.2, 3.0, 2.6, 2.6, 1.1),            // Временно.
            new BattleDifficulty("SUPER ULTRA MEGA HARD", 4, 6.0, 5.0, 5.0, 1.2),   // Временно.
        };

        public BattleDifficulty(string name, double itemDamageKoef, double expMultiplier, double goldMultiplier, double dropMultiplier, double itemAdditionalKoef)
        {
            Name = name;
            ItemDamageKoef = itemDamageKoef;
            ExpMultiplier = expMultiplier;
            GoldMultiplier = goldMultiplier;
            DropMultiplier = dropMultiplier;
            ItemAdditionalKoef = itemAdditionalKoef;
        }

        public static BattleDifficulty GetCurrent() 
            => BattleDifficulties[CurrentIndex];

        public static void SetBattleDifficulty(int battleDifficultyEnum)
            => CurrentIndex = battleDifficultyEnum;

        public static int CurrentIndex { get; private set; } = 0;

        public static string[] GetStrings()
            => BattleDifficulties.Select(s => s.Name).ToArray();

    }
}
