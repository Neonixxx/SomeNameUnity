using SomeName.Core.Domain;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;
using SomeName.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeName.Core
{
    public class ForgeService
    {
        public InventoryService InventoryService { get; set; }

        public EnchantManager EnchantManager { get; set; } = EnchantManager.Standard;

        public ForgeService(Player player)
        {
            InventoryService = player.InventoryService;
        }

        /// <summary>
        /// Улучшить предмет с помощью соответствующего свитка улучшения.
        /// </summary>
        /// <param name="itemToEnchant">Предмет.</param>
        /// <param name="scrollOfEnchant">Свиток улучшения.</param>
        /// <returns><c>true</c>, если улучшение прошло успешно, иначе - <c>false</c>.</returns>
        public bool EnchantItem(ICanBeEnchanted itemToEnchant, ScrollOfEnchant scrollOfEnchant)
        {
            if (!CanBeEnchantedWith(itemToEnchant, scrollOfEnchant))
                throw new ArgumentException($"Предмет типа {itemToEnchant.GetType()} нельзя улучшить с помощью {scrollOfEnchant.GetType()}");

            if (!InventoryService.BagContains(scrollOfEnchant))
                throw new InvalidOperationException($"{nameof(scrollOfEnchant)} не содержится в {nameof(Player.Inventory)}");

            var isItemInInventory = InventoryService.BagContains(itemToEnchant);
            var isItemEquipped = InventoryService.IsEquipped(itemToEnchant);

            if (!isItemInInventory && !isItemEquipped)
                throw new InvalidOperationException($"{nameof(itemToEnchant)} не содержится в {nameof(Player.Inventory)}");

            InventoryService.Remove(scrollOfEnchant);
            var enchantResult = EnchantManager.TryEnchant(itemToEnchant, scrollOfEnchant);

            if (!enchantResult)
            {
                if (isItemInInventory)
                    InventoryService.Remove(scrollOfEnchant);
                else
                    InventoryService.Remove(itemToEnchant);
            }

            return enchantResult;
        }

        public bool CanBeEnchantedWith(ICanBeEnchanted itemToEnchant, ScrollOfEnchant scrollOfEnchant)
        {
            if (scrollOfEnchant is ScrollOfEnchantWeapon && itemToEnchant is Weapon
                || scrollOfEnchant is ScrollOfEnchantArmor && itemToEnchant is Armor)
                return true;

            return false;
        }
    }
}
