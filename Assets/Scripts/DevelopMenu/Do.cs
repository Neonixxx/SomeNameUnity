using System;
using System.Linq;
using SomeName.Core.Balance;
using SomeName.Core.Domain;
using SomeName.Core.Forge.Cube;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;
using SomeName.Core.Locations;
using SomeName.Core.Monsters.Interfaces;
using SomeName.Core.Services;
using UnityEngine;
using UnityEngine.UI;

namespace DepelopMenu
{
    public class Do : MonoBehaviour
    {
        public Dropdown CommandsDropDown;
        public InputField ArgumentsInputField;

        private DropService _dropService = DropService.Standard;
        private MonsterStatsBalance _monsterStatsBalance = MonsterStatsBalance.Get(MonsterType.Boss);
        private InventoryService _inventoryService;
        private SomeName.Core.Services.LocationService _locationService;
        private Player _player;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Parse);
            _player = FindObjectOfType<GameState>().Player;
            _inventoryService = _player.InventoryService;
            _locationService = _player.LocationService;
        }

        private void Parse()
        {
            switch (CommandsDropDown.value)
            {
                case 0:
                    KillBoss();
                    break;
                case 1:
                    GetItem();
                    break;
                case 2:
                    SetEnchantmentLevel();
                    break;
                case 3:
                    OpenLocation();
                    break;
            }
        }

        private string[] GetArgs()
            => ArgumentsInputField.text
                .Split(new string[] { ",", ", ", " " }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

        private void KillBoss()
        {
            var args = GetArgs();

            if (args.Length == 0 || args.Length > 2)
                return;

            var bossLevel = Convert.ToInt32(args[0]);
            var count = 1;

            if (args.Length == 2)
                count = Convert.ToInt32(args[1]);

            for (int i = 0; i < count; i++)
            {
                var dropValue = _monsterStatsBalance.GetDefaultDropValue(bossLevel);
                var drop = _dropService.Build(bossLevel, dropValue);
                _player.TakeDrop(drop);
            }
        }

        private void GetItem()
        {
            var args = GetArgs();

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
                case "helmet":
                    item = new SimpleHelmet();
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
            item.UpdateGoldValueKoef();

            _inventoryService.Add(item);
        }

        private void SetEnchantmentLevel()
        {
            var args = GetArgs();

            if (args.Length > 2)
                return;

            var itemIndex = _inventoryService.Count - 1;
            int enchantmentLevel;
            if (args.Length == 1)
                enchantmentLevel = Convert.ToInt32(args[0]);
            else if (args.Length == 2)
            {
                if (args[0] != "last")
                    itemIndex = Convert.ToInt32(args[0]);
                enchantmentLevel = Convert.ToInt32(args[1]);
            }
            else
                return;

            var item = _inventoryService.Get(itemIndex);
            new EnchantManager().SetEnchantmentLevel((ICanBeEnchanted)item, enchantmentLevel);
        }

        private void OpenLocation()
        {
            var args = GetArgs();

            if (args.Length == 1)
            {
                if (args[0] == "all")
                {
                    Location.BaseLocations.Where(l => !_player.LocationsInfo.Contains(l.Id))
                        .Select(l => l.Id)
                        .ToList()
                        .ForEach(l => _player.LocationsInfo.Add(l));
                }
                else
                {
                    var locationId = Convert.ToInt32(args[0]);
                    if (!_player.LocationsInfo.Contains(locationId))
                        _player.LocationsInfo.Add(locationId);
                }
            }
        }
    }
}