using HmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManegementSystem.ViewModels
{
    public class IndexViewModels
    {
        public List<AccomodationType> AccomodationTypes { get; set; }
        public List<AccomodationPackage> AccomodationPackages { get; internal set; }

        public List<Amenity> Amenities { get; set; }
    }
}