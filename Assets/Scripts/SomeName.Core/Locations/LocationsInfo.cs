using System.Collections.Generic;
using System.Linq;

namespace SomeName.Core.Locations
{
    public class LocationsInfo
    {
        public List<LocationInfo> OpenedLocationInfoes { get; set; } = new List<LocationInfo>();

        public int CurrentLocationId { get; set; }


        public void Add(int locationId)
            => OpenedLocationInfoes.Add(new LocationInfo { Id = locationId });

        public bool Contains(int locationId)
            => OpenedLocationInfoes.Any(s => s.Id == locationId);

        public LocationInfo GetById(int locationId)
            => OpenedLocationInfoes.First(s => s.Id == locationId);
    }
}
