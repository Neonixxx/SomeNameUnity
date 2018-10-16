using System.Collections.Generic;
using System.Linq;

namespace SomeName.Core.Locations
{
    public class LocationsInfo
    {
        public List<LocationInfo> OpenedLocationIds { get; set; } = new List<LocationInfo> { new LocationInfo { Id = 1 } };

        public int CurrentLocationId { get; set; }


        public void Add(int locationId)
            => OpenedLocationIds.Add(new LocationInfo { Id = locationId });

        public bool Contains(int locationId)
            => OpenedLocationIds.Any(s => s.Id == locationId);

        public LocationInfo GetById(int locationId)
            => OpenedLocationIds.First(s => s.Id == locationId);
    }
}
