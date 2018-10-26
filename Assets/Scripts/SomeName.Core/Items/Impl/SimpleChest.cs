using SomeName.Core.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SomeName.Core.Items.Impl
{
    public class SimpleChest : Chest
    {
        public SimpleChest()
        {
            Description = "Кожанный жилет";
            ImageId = "SimpleChest";
        }

        public override IItem Clone()
        {
            var item = new SimpleChest();
            base.CloneTo(item);
            return item;
        }
    }
}
