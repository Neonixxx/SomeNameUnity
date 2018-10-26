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
        public string ImageId { get; set; }

        [JsonIgnore]
        public bool CanStack { get; set; }

        public int Quantity { get; set; }

        public override string ToString()
            => $"{Description}" +
                $"{NewLine}Level: {Level}";

        public abstract IItem Clone();

        protected void CloneTo(Item item)
        {
            item.Level = Level;
            item.GoldValue = GoldValue.Clone();
            item.Description = Description;
            item.ImageId = ImageId;
            item.CanStack = CanStack;
            item.Quantity = Quantity;
        }
    }
}
