using SomeName.Core.Domain;
using UnityEngine;

namespace SomeName.Core.Items.Interfaces
{
    public interface IItem
    {
        int Level { get; set; }

        BaseKoefValue<long> GoldValue { get; set; }

        void UpdateGoldValueKoef();

        string Description { get; set; }

        string ImageId { get; set; }
    }
}
