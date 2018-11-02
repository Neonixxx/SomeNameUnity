using System;
using System.Collections.Generic;
using System.Linq;

namespace SomeName.Core
{
    public enum ItemType
    {
        Equippment = 0,
        Armor = 1,
        Weapon = 2,
        Chest = 3,
        Gloves = 4,
        Helmet = 5,


        Consumable = 20,
        ScrollOfEnchant = 21,
        ScrollOfEnchantWeapon = 22,
        ScrollOfEnchantArmor = 23,
    }

    public static class ItemTypeExtensions
    {
        private static readonly List<Tuple<ItemType, ItemType>> ItemTypeRelations = new List<Tuple<ItemType, ItemType>>
        {
            Tuple.Create(ItemType.Armor, ItemType.Equippment),
            Tuple.Create(ItemType.Weapon, ItemType.Equippment),
            Tuple.Create(ItemType.Chest, ItemType.Armor),
            Tuple.Create(ItemType.Gloves, ItemType.Armor),
            Tuple.Create(ItemType.Helmet, ItemType.Armor),
            Tuple.Create(ItemType.Chest, ItemType.Equippment),
            Tuple.Create(ItemType.Gloves, ItemType.Equippment),
            Tuple.Create(ItemType.Helmet, ItemType.Equippment),
            Tuple.Create(ItemType.ScrollOfEnchantWeapon, ItemType.ScrollOfEnchant),
            Tuple.Create(ItemType.ScrollOfEnchantArmor, ItemType.ScrollOfEnchant),
        };

        public static bool CanBeFrom(this ItemType itemType, ItemType secondItemType)
        {
            if (itemType == secondItemType || ItemTypeRelations.FirstOrDefault(s => s.Item1 == itemType && s.Item2 == secondItemType) != null)
                return true;

            return false;
        }
    }
}