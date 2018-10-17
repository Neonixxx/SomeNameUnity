using System.Collections.Generic;
using Newtonsoft.Json;
using SomeName.Core.Balance;
using SomeName.Core.Domain;
using SomeName.Core.Items.Impl;
using SomeName.Core.Locations;
using UnityEngine;

namespace SomeName.Core.IO
{
    public class PlayerIO
    {
        public Crypto Crypto { get; set; } = new Crypto();

        public Player StartNew()
        {
            var player = new Player()
            {
                Level = new Level { Normal = 1 },
                Exp = 0,
                ExpForNextLevel = DropBalance.Standard.GetExp(1),
                Inventory = new Domain.Inventory
                {
                    Gold = 0,
                    EquippedItems = new EquippedItems { Weapon = new BeginnerSword() },
                },
                LocationsInfo = new LocationsInfo
                {
                    OpenedLocationIds = new List<Locations.LocationInfo> { new Locations.LocationInfo { Id = 1 } },
                    CurrentLocationId = 1,
                },
            };
            player.Initialize();
            return player;
        }

        public void Save(Player player)
        {
            var data = JsonConvert.SerializeObject(player, Formatting.None, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });
            var encryptedData = Crypto.Encrypt(data);
            PlayerPrefs.SetString("Player", encryptedData);
        }

        public bool TryLoad(out Player player)
        {
            try
            {
                var encryptedData = PlayerPrefs.GetString("Player");
                var data = Crypto.Decrypt(encryptedData);
                player = JsonConvert.DeserializeObject<Player>(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
                player.Initialize();
            }
            catch (JsonSerializationException)
            {
                player = null;
                return false;
            }

            return true;
        }
    }
}
