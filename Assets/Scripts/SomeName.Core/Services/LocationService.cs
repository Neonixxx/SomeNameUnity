using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Locations;
using SomeName.Core.Monsters.Interfaces;

namespace SomeName.Core.Services
{
    public class LocationService
    {
        public LocationService(LocationsInfo locationsInfo)
        {
            LocationsInfo = locationsInfo;
            _currentLocation = Location.BaseLocations.First(s => s.Id == LocationsInfo.CurrentLocationId);
        }

        public LocationsInfo LocationsInfo { get; set; }

        private Location _currentLocation;
        private Monster _currentMonster;

        public Monster GetMonster()
        {
            _currentMonster = GetCurrentLocation().GetMonster();
            return _currentMonster;
        }

        public Monster GetBoss()
        {
            _currentMonster = GetCurrentLocation().GetBoss();
            return _currentMonster;
        }

        public Location GetCurrentLocation()
            => _currentLocation;

        public List<Location> GetOpenedLocations()
        {
            return LocationsInfo.OpenedLocationIds.Join(Location.BaseLocations
                , li => li.Id
                , bl => bl.Id
                , (li, bl) => bl).OrderBy(s => s.Id).ToList();
        }

        public bool MoveTo(int id)
        {
            if (!LocationsInfo.Contains(id))
                return false;

            LocationsInfo.CurrentLocationId = id;
            _currentLocation = Location.BaseLocations.First(s => s.Id == LocationsInfo.CurrentLocationId);
            return true;
        }

        /// <summary>
        /// Обработать убийство монстра.
        /// Вызвать, если монстр был убит.
        /// </summary>
        public void MonsterKilled()
        {
            var locationInfo = LocationsInfo.GetById(_currentLocation.Id);
            switch (_currentMonster.MonsterType)
            {
                case MonsterType.Normal:
                    locationInfo.NormalMonstersKilledCount++;
                    break;
                case MonsterType.Elite:
                    locationInfo.EliteMonstersKilledCount++;
                    break;
                case MonsterType.Boss:
                    locationInfo.BossKilledCount++;
                    break;
            }

            var newLocationId = locationInfo.Id + 1;
            if (locationInfo.BossKilledCount > 0 && Location.BaseLocations.Any(s => s.Id == newLocationId) && !LocationsInfo.Contains(newLocationId))
                LocationsInfo.Add(newLocationId);
        }
    }
}
