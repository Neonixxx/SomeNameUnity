using System;
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
            _currentMonster = _currentLocation.GetMonster();
            return _currentMonster;
        }

        public int GetCurrentFightBossValue()
            => LocationsInfo.GetById(_currentLocation.Id).FightBossValue;

        public int GetRequiredFightBossValue()
            => _currentLocation.RequiredFightBossValue;

        public bool CanFightBoss()
            => GetCurrentFightBossValue() >= GetRequiredFightBossValue();

        public Monster GetBoss()
        {
            if (!CanFightBoss())
                throw new InvalidOperationException();

            LocationsInfo.GetById(_currentLocation.Id).FightBossValue -= _currentLocation.RequiredFightBossValue;

            _currentMonster = _currentLocation.GetBoss();
            return _currentMonster;
        }

        public Location GetCurrentLocation()
            => _currentLocation;

        public List<Location> GetOpenedLocations()
            => LocationsInfo.OpenedLocationIds.Join(Location.BaseLocations
                , li => li.Id
                , bl => bl.Id
                , (li, bl) => bl).OrderBy(s => s.Id).ToList();

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
                    locationInfo.FightBossValue = Math.Min(locationInfo.FightBossValue + 1, GetRequiredFightBossValue());
                    break;
                case MonsterType.Elite:
                    locationInfo.EliteMonstersKilledCount++;
                    locationInfo.FightBossValue = Math.Min(locationInfo.FightBossValue + 3, GetRequiredFightBossValue());
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
