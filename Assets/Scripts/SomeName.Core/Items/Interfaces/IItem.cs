﻿using SomeName.Core.Domain;
using UnityEngine;

namespace SomeName.Core.Items.Interfaces
{
    public interface IItem
    {
        ItemType ItemType { get; }

        int Level { get; set; }

        BaseKoefValue<long> GoldValue { get; set; }

        void UpdateGoldValueKoef();

        string Description { get; set; }

        string ImageId { get; set; }

        int Quantity { get; set; }

        int MaxQuantity { get; set; }

        IItem Clone();
    }
}
