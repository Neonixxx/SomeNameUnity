using Newtonsoft.Json;
using SomeName.Core.Items.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core.Domain
{
    [JsonObject]
    public class EquippedItems : IEnumerable<IEquippment>
    {
        public Weapon Weapon { get; set; }

        public Chest Chest { get; set; }

        public Gloves Gloves { get; set; }

        public long GetDefence()
            => this.Sum(i => (i as Armor)?.Defence.Value ?? 0);

        public int GetPower()
            => this.Sum(i => i?.Bonuses.Power.Value ?? 0);

        public int GetVitality()
            => this.Sum(i => i?.Bonuses.Vitality.Value ?? 0);

        public int GetAccuracy()
            => this.Sum(i => i?.Bonuses.Accuracy.Value ?? 0);

        public int GetEvasion()
            => this.Sum(i => i?.Bonuses.Evasion.Value ?? 0);

        public double GetCritChance()
            => this.Sum(i => i?.Bonuses.CritChance.Value ?? 0);

        public double GetCritDamage()
            => this.Sum(i => i?.Bonuses.CritDamage.Value ?? 0);

        public long GetHealthPerHit()
            => this.Sum(i => i?.Bonuses.HealthPerHit.Value ?? 0);

        public IEnumerator<IEquippment> GetEnumerator()
        {
            yield return Weapon;
            yield return Chest;
            yield return Gloves;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public void Remove(IEquippment equippment)
        {
            if (equippment == Weapon)
                Weapon = null;

            if (equippment == Chest)
                Chest = null;

            if (equippment == Gloves)
                Gloves = null;
        }
    }
}
