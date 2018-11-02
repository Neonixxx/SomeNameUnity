using System;
using System.Text;
using SomeName.Core.Domain;
using static System.Environment;

namespace SomeName.Core.Items.Bonuses
{
    // TODO : Объеденить статы в отдельный класс.
    public class ItemBonuses
    {
        public BaseKoefValue<int> Power { get; set; } = new BaseKoefValue<int>();

        public BaseKoefValue<int> Vitality { get; set; } = new BaseKoefValue<int>();

        public BaseKoefValue<int> Accuracy { get; set; } = new BaseKoefValue<int>();

        public BaseKoefValue<int> Evasion { get; set; } = new BaseKoefValue<int>();

        public BaseKoefValue<double> CritChance { get; set; } = new BaseKoefValue<double>();

        public BaseKoefValue<double> CritDamage { get; set; } = new BaseKoefValue<double>();

        public BaseKoefValue<long> HealthPerHit { get; set; } = new BaseKoefValue<long>();

        public double GetValueKoef()
            => Math.Sqrt(Power.Koef.OneIfZero() * Vitality.Koef.OneIfZero()
                * Accuracy.Koef.OneIfZero() * Evasion.Koef.OneIfZero()
                * CritChance.Koef.OneIfZero() * CritDamage.Koef.OneIfZero()
                * HealthPerHit.Koef.OneIfZero());

        public override string ToString()
        {
            var result = new StringBuilder();

            if (Power.Value != 0)
                result.Append($"Power: {Power.Value}");
            if (Vitality.Value != 0)
            {
                if (result.Length != 0)
                    result.Append($"{NewLine}");
                result.Append($"Vitality: {Vitality.Value}");
            }
            if (Accuracy.Value != 0)
            {
                if (result.Length != 0)
                    result.Append($"{NewLine}");
                result.Append($"Accuracy: {Accuracy.Value}");
            }
            if (Evasion.Value != 0)
            {
                if (result.Length != 0)
                    result.Append($"{NewLine}");
                result.Append($"Evasion: {Evasion.Value}");
            }
            if (CritChance.Value != 0)
            {
                if (result.Length != 0)
                    result.Append($"{NewLine}");
                result.Append($"Шанс крита: {CritChance.Value.ToPercentString(1)}");
            }
            if (CritDamage.Value != 0)
            {
                if (result.Length != 0)
                    result.Append($"{NewLine}");
                result.Append($"Сила крита: {CritDamage.Value.ToPercentString(1)}");
            }
            if (HealthPerHit.Value != 0)
            {
                if (result.Length != 0)
                    result.Append($"{NewLine}");
                result.Append($"Health per hit: {HealthPerHit.Value}");
            }

            return result.ToString();
        }

        public ItemBonuses Clone()
            => new ItemBonuses
            {
                Power = Power.Clone(),
                Vitality = Vitality.Clone(),
                Accuracy = Accuracy.Clone(),
                Evasion = Evasion.Clone(),
                CritChance = CritChance.Clone(),
                CritDamage = CritDamage.Clone(),
                HealthPerHit = HealthPerHit.Clone()
            };
    }
}
