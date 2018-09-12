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

namespace SomeName.Core
{
    public static class PlayerIO
    {
        public static string SavePath { get; set; } = @"Saves\PlayerSave.dat";

        public static Player StartNew()
        {
            return new Player()
            {
                Level = 1,
                Exp = 0,
                ExpForNextLevel = DropBalance.Standard.GetExp(1),
                Gold = 0,
                EquippedItems = new EquippedItems { Weapon = new BeginnerSword() },
                Inventory = new List<IItem>()
            };
        }

        // TODO : Добавить зашифровку данных.
        public static bool TrySave(Player player)
        {
            var data = JsonConvert.SerializeObject(player, Formatting.None, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });
            var stream = new FileStream(SavePath, FileMode.OpenOrCreate);
            var binaryFormatter = new BinaryFormatter();
            try
            {
                binaryFormatter.Serialize(stream, data);
            }
            catch (SerializationException)
            {
                return false;
            }
            finally
            {
                stream.Close();
            }

            return true;
        }

        public static bool TryLoad(out Player player)
        {
            var stream = new FileStream(SavePath, FileMode.OpenOrCreate);
            var binaryFormatter = new BinaryFormatter();
            try
            {
                var data = (string)binaryFormatter.Deserialize(stream);
                player = JsonConvert.DeserializeObject<Player>(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            }
            catch (JsonSerializationException)
            {
                player = null;
                return false;
            }
            finally
            {
                stream.Close();
            }

            return true;
        }
    }
}
