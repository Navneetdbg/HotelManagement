using HmsEntity;
using HotelManegementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManegementSystem.Areas.DashBoard.ViewModel
{
    public class AccomodationPackagesModels
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int NoOfRooms { get; set; }

        public int AccomodationTypeId { get; set; }

        public string Details { get; set; }
        public AccomodationType AccomodationType { get; set; }
        public decimal PerNight { get; set; }

        public string PicTureIds { get; set; }
        public List<AccomodationType> AccomodationTypes { get; set; }

        public List<AccomodationPackageImage> AccomodationPackageImages { get; set; }
    }

    public class AccomodationPackagesListing
    {
        public List<AccomodationPackage> AccomodationPackages { get; set; }
        public string Search { get; set; }
        public List<AccomodationType> AccomodationTypes { get;  set; }
        public int? AccomodationTypeId { get;  set; }

        public string PictureIds { get; set; }
        public Pager pager { get; set; }
    }

    public class AccomodationPackageAmnetiesVM
    {
        public int Id { get; set; }

        public List<Amenity> Amenities { get; set; }

        public List<AccomodationPackageAmenity> CurrentAmenities { get; set; }
    }
}