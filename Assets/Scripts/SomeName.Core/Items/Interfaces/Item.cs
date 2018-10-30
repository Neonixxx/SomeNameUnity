using Newtonsoft.Json;
using SomeName.Core.Domain;
using static System.Environment;

namespace SomeName.Core.Items.Interfaces
{
    public abstract class Item : IItem
    {
        [JsonIgnore]
        public ItemType ItemType { get; protected set; }

        public int Level { get; set; }

        public BaseKoefValue<long> GoldValue { get; set; } = new BaseKoefValue<long>();

        public virtual void UpdateGoldValueKoef()
            => GoldValue.Koef = 1.0;

        public string Description { get; set; }

        [JsonIgnore]
        public string ImageId { get; set; }

        public int Quantity { get; set; }

        [JsonIgnore]
        public int MaxQuantity { get; set; }

        public override string ToString()
        {
            var result = $"{Description}" +
                $"{NewLine}Level: {Level}";
            if (Quantity > 1)
                result += $"{NewLine}Quantity: {Quantity}/{MaxQuantity}";
            return result;
        }

        public abstract IItem Clone();

        protected void CloneTo(Item item)
        {
            item.Level = Level;
            item.GoldValue = GoldValue.Clone();
            item.Description = Description;
            item.ImageId = ImageId;
            item.Quantity = Quantity;
            item.MaxQuantity = MaxQuantity;
        }
    }
}
