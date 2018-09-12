using SomeName.Core.Items.Bonuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core.Items.Interfaces
{
    public interface IEquippment : IItem
    {
        ItemBonuses Bonuses { get; set; }
    }
}
