using HmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManegementSystem.ViewModels
{
    public class AccomodationPackagesViewModel
    {
        public AccomodationPackage AccomodationPackage { get; set; }
        public List<AccomodationPackage> AccomodationPackages { get; set; }

        public List<Amenity> Amenities { get; set; }


    }
}