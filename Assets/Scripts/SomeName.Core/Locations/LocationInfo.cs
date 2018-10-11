using System.Collections.Generic;

namespace SomeName.Core.Locations
{
    public class LocationInfo
    {
        public List<int> OpenedLocationIds { get; set; } = new List<int>();

        public int CurrentLocationId { get; set; }
    }
}
