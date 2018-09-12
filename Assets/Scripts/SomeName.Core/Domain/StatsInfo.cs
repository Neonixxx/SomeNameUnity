using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

namespace SomeName.Core.Domain
{
    public class StatsInfo
    {
        public int Level { get; set; }

        public int Power { get; set; }

        public int Vitality { get; set; }

        public int Accuracy { get; set; }

        public int Evasion { get; set; }

        public long Damage { get; set; }

        public long Defence { get; set; }

        public double DefenceKoef { get; set; }

        public long MaxHealth { get; set; }

        public double CritChance { get; set; }

        public double CritDamage { get; set; }

        public long HealthPerHit { get; set; }

        public override string ToString()
        {
            return $"Level: {Level}" +
                $"{NewLine}Damage: {Damage}" +
                $"{NewLine}Health: {MaxHealth}" +
                $"{NewLine}HealthPerHit: {HealthPerHit}" +
                $"{NewLine}Power: {Power}" +
                $"{NewLine}Vitality: {Vitality}" +
                $"{NewLine}Accuracy: {Accuracy}" +
                $"{NewLine}Evasion: {Evasion}" +
                $"{NewLine}Defence: {Defence}" +
                $"{NewLine}Снижение получаемого урона: {DefenceKoef.ToPercentString(2)}" +
                $"{NewLine}Шанс крита: {CritChance.ToPercentString(1)}" +
                $"{NewLine}Сила крита: {CritDamage.ToPercentString(1)}";
        }
    }
}
