using SomeName.Core.Items.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeName.Core.Items.Interfaces;
using SomeName.Core.Balance;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SomeName.Core.Domain;
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
                Level = 1,
                Exp = 0,
                ExpForNextLevel = DropBalance.Standard.GetExp(1),
                Inventory = new Domain.Inventory
                {
                    Gold = 0,
                    Bag = new List<IItem>(),
                    EquippedItems = new EquippedItems { Weapon = new BeginnerSword() },
                    Cube = new List<IItem>()
                }
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
