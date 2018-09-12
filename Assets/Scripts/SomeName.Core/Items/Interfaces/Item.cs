using Newtonsoft.Json;
using SomeName.Core.Domain;
using UnityEngine;
using static System.Environment;

namespace SomeName.Core.Items.Interfaces
{
    public abstract class Item : IItem
    {
        public int Level { get; set; }

        public BaseKoefValue<long> GoldValue { get; set; } = new BaseKoefValue<long>();

        public virtual void UpdateGoldValueKoef()
            => GoldValue.Koef = 1.0;

        public string Description { get; set; }

        [JsonIgnore]
        public Sprite Image { get; set; }

        public override string ToString()
            => $"{Description}" +
                $"{NewLine}Level: {Level}";
    }
}
