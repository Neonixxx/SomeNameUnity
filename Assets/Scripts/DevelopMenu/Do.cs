using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SomeName.Core.Difficulties;
using SomeName.Core.Domain;
using SomeName.Core.Forge.Cube;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;
using SomeName.Core.Services;
using UnityEngine;
using UnityEngine.UI;

namespace DepelopMenu
{
    public class Do : MonoBehaviour
    {
        public Dropdown CommandsDropDown;
        public InputField ArgumentsInputField;

        private List<BattleDifficulty> _difficulties;
        private InventoryService _inventoryService;
        // Use this for initialization
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Parse);
            _difficulties = (List<BattleDifficulty>)(typeof(BattleDifficulty)
                .GetField("BattleDifficulties", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null));
            _inventoryService = FindObjectOfType<GameState>().Player.InventoryService;
        }

        private void Parse()
        {
            switch (CommandsDropDown.value)
            {
                case 0:
                    AddDifficulty();
                    break;
                case 1:
                    GetItem();
                    break;
                case 2:
                    SetEnchantmentLevel();
                    break;

            }
        }

        private void AddDifficulty()
        {
            var args = ArgumentsInputField.text
                .Split(new string[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            if (args.Length != 6)
                return;

            var difficultyName = args[0];
            var koefs = args.Skip(1)
                .Select(s => Convert.ToDouble(s))
                .ToArray();
            _difficulties.Add(new BattleDifficulty(difficultyName, koefs[0], koefs[1], koefs[2], koefs[3], koefs[4]));
        }

        private void GetItem()
        {
            var args = ArgumentsInputField.text
                .Split(new string[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            if (args.Length != 10)
                return;

            var itemType = args[0];
            var itemStats = args.Skip(1)
                .Select(s => Convert.ToDouble(s))
                .ToArray();

            Equippment item = null;
            switch(itemType)
            {
                case "weapon":
                    item = new SimpleSword();
                    break;
                case "chest":
                    item = new SimpleChest();
                    break;
                case "gloves":
                    item = new SimpleGloves();
                    break;
            }
            item.Level = (int)itemStats[0];
            item.MainStat.Base = (long)itemStats[1];
            item.MainStat.Koef = 1.0;
            item.MainStat.EnchantKoef = 1.0;
            item.Bonuses = new SomeName.Core.Items.Bonuses.ItemBonuses();
            item.Bonuses.Power.Base = (int)itemStats[2];
            item.Bonuses.Power.Koef = 1.0;
            item.Bonuses.Vitality.Base = (int)itemStats[3];
            item.Bonuses.Vitality.Koef = 1.0;
            item.Bonuses.Accuracy.Base = (int)itemStats[4];
            item.Bonuses.Accuracy.Koef = 1.0;
            item.Bonuses.Evasion.Base = (int)itemStats[5];
            item.Bonuses.Evasion.Koef = 1.0;
            item.Bonuses.CritChance.Base = itemStats[6];
            item.Bonuses.CritChance.Koef = 1.0;
            item.Bonuses.CritDamage.Base = itemStats[7];
            item.Bonuses.CritDamage.Koef = 1.0;
            item.Bonuses.HealthPerHit.Base = (long)itemStats[8];
            item.Bonuses.HealthPerHit.Koef = 1.0;

            _inventoryService.Add(item);
        }

        private void SetEnchantmentLevel()
        {
            var args = ArgumentsInputField.text
                .Split(new string[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            if (args.Length != 2)
                return;

            var itemIndex = args[0] == "last"
                ? _inventoryService.Count - 1
                : Convert.ToInt32(args[0]);
            var enchantmentLevel = Convert.ToInt32(args[1]);

            var item = _inventoryService.Get(itemIndex);
            new EnchantManager().SetEnchantmentLevel((ICanBeEnchanted)item, enchantmentLevel);
        }
    }
}