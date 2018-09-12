using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeName.Core.Items.Interfaces;
using SomeName.Core.Balance;
using SomeName.Core.Domain;
using SomeName.Core.Items.Impl;
using Newtonsoft.Json;

namespace SomeName.Core.Domain
{
    // TODO : Сделать конвертер Player -> StatsInfo.
    public class Player : IAttacker, IAttackTarget
    {
        [JsonIgnore]
        public PlayerStatsCalculator PlayerStatsCalculator { get; set; }

        [JsonIgnore]
        public AttackManager AttackManager { get; set; }

        public Player()
        {
            PlayerStatsCalculator = PlayerStatsCalculator.Standard;
            AttackManager = new AttackManager(this);
        }

        public int Level { get; set; }

        public long ExpForNextLevel { get; set; }

        public long Exp { get; set; }

        public long Gold { get; set; }

        public long Health { get; set; }

        public bool IsDead { get; set; }

        public EquippedItems EquippedItems { get; set; }

        public List<IItem> Inventory { get; set; }

        public long GetDamage()
            => PlayerStatsCalculator.CalculateDamage(this);

        public long GetDefence()
            => PlayerStatsCalculator.CalculateDefence(this);

        public double GetDefenceKoef()
            => PlayerStatsCalculator.CalculateDefenceKoef(this);

        public long GetMaxHealth()
            => PlayerStatsCalculator.CalculateMaxHealth(this);

        public int GetPower()
            => PlayerStatsCalculator.CalculatePower(this);

        public int GetVitality()
            => PlayerStatsCalculator.CalculateVitality(this);

        public int GetAccuracy()
            => PlayerStatsCalculator.CalculateAccuracy(this);

        public int GetEvasion()
            => PlayerStatsCalculator.CalculateEvasion(this);

        public double GetCritChance()
            => PlayerStatsCalculator.CalculateCritChance(this);

        public double GetCritDamage()
            => PlayerStatsCalculator.CalculateCritDamage(this);

        public long GetHealthPerHit()
            => PlayerStatsCalculator.CalculateHealthPerHit(this);


        public void OnHit()
        {
            var healthResult = Health + GetHealthPerHit();
            var maxHealth = GetMaxHealth();
            Health = healthResult > maxHealth
                ? maxHealth
                : healthResult;
        }


        public void Respawn()
        {
            Health = GetMaxHealth();
            IsDead = false;
        }

        public void SellItem(ShopManager shopService, IItem item)
        {
            if (Inventory.Contains(item))
            {
                Inventory.Remove(item);
                Gold += shopService.GetSellItemValue(item);
            }
        }

        public long Attack(IAttackTarget attackTarget)
            => AttackManager.Attack(attackTarget);

        // TODO : Решить, как можно сделать метод более расширяемым к добавлению новых типов предметов.
        public bool Equip(IItem item)
        {
            if (item as Weapon != null)
            {
                if (EquippedItems.Weapon != null)
                    Inventory.Add(EquippedItems.Weapon);
                Inventory.Remove(item);
                EquippedItems.Weapon = (Weapon)item;
                return true;
            }
            if (item as Weapon != null)
            {
                if (EquippedItems.Chest != null)
                    Inventory.Add(EquippedItems.Chest);
                Inventory.Remove(item);
                EquippedItems.Chest = (Chest)item;
                return true;
            }
            if (item as Weapon != null)
            {
                if (EquippedItems.Gloves != null)
                    Inventory.Add(EquippedItems.Gloves);
                Inventory.Remove(item);
                EquippedItems.Gloves = (Gloves)item;
                return true;
            }

            return false;
        }

        public void Unequip(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    if (EquippedItems.Weapon != null)
                    {
                        Inventory.Add(EquippedItems.Weapon);
                        EquippedItems.Weapon = null;
                    }
                    break;

                case ItemType.Chest:
                    if (EquippedItems.Chest != null)
                    {
                        Inventory.Add(EquippedItems.Chest);
                        EquippedItems.Chest = null;
                    }
                    break;

                case ItemType.Gloves:
                    if (EquippedItems.Gloves != null)
                    {
                        Inventory.Add(EquippedItems.Gloves);
                        EquippedItems.Gloves = null;
                    }
                    break;
            }
        }

        public void TakeDrop(Drop drop)
        {
            TakeExp(drop.Exp);
            Gold += drop.Gold;
            TakeItems(drop.Items);
        }

        public void TakeExp(long exp)
        {
            var totalExp = Exp + exp;
            while (totalExp >= ExpForNextLevel)
            {
                totalExp -= ExpForNextLevel;
                Level++;
                ExpForNextLevel = DropBalance.Standard.GetExp(Level);
            }
            Exp = totalExp;
        }

        public void TakeItem(IItem item)
            => Inventory.Add(item);

        public void TakeItems(List<IItem> items)
            => Inventory.AddRange(items);
    }
}
