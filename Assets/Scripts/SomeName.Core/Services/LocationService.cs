using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Locations;
using SomeName.Core.Monsters.Interfaces;

namespace SomeName.Core.Services
{
    public class LocationService
    {
        public LocationService(LocationInfo locationInfo)
        {
            LocationInfo = locationInfo;
            _currentLocation = Location.BaseLocations.First(s => s.Id == LocationInfo.CurrentLocationId);
        }

        public LocationInfo LocationInfo { get; set; }

        private Location _currentLocation;
        private Monster _currentMonster;

        public Monster GetMonster()
        {
            _currentMonster = GetCurrentLocation().GetMonster();
            return _currentMonster;
        }

        public Location GetCurrentLocation()
            => _currentLocation;

        public List<Location> GetOpenedLocationStrings()
        {
            return LocationInfo.OpenedLocationIds.Join(Location.BaseLocations
                , id => id
                , bl => bl.Id
                , (id, bl) => bl).OrderBy(s => s.Id).ToList();
        }

        public bool MoveTo(int id)
        {
            if (!LocationInfo.OpenedLocationIds.Contains(id))
                return false;

            LocationInfo.CurrentLocationId = id;
            _currentLocation = Location.BaseLocations.First(s => s.Id == LocationInfo.CurrentLocationId);
            return true;
        }

        /// <summary>
        /// Обработать убийство монстра.
        /// Вызвать, если монстр был убит.
        /// </summary>
        public void MonsterKilled()
        {
            var newLocationId = _currentLocation.Id + 1;
            if (Location.BaseLocations.Any(s => s.Id == newLocationId) && !LocationInfo.OpenedLocationIds.Contains(newLocationId))
                LocationInfo.OpenedLocationIds.Add(newLocationId);
        }
    }
}
