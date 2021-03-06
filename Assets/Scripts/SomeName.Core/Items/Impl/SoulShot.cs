﻿using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Items.Impl
{
    public class SoulShot : Consumable
    {
        public double BonusDamageKoef => 0.65;

        public bool IsActivated { get; set; }

        public SoulShot()
            : base()
        {
            Description = "Заряд души";
            ImageId = "SoulShot";
            MaxQuantity = 50000;
        }

        public override IItem Clone()
        {
            var item = new SoulShot();
            base.CloneTo(item);
            return item;
        }
    }
}
