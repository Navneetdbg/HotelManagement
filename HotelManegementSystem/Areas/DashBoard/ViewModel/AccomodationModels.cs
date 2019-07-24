using HmsEntity;
using HotelManegementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManegementSystem.Areas.DashBoard.ViewModel
{
    public class AccomodationModels
    {
        public int Id { get; set; }
        public int AccomodationPackageId { get; set; }
        public virtual AccomodationPackage AccomodationPackage { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsAvailable { get; set; }
        public List<AccomodationPackage> AccomodationPackages { get; set; }

        public string PicTureIds { get; set; }
        public List<AccomodationImages> AccomodationImages { get; set; }
    }

    public class AccomodationListingModels
    {
        public List<Accomodation> Accomodations { get; set; }
        public string Search { get; set; }
        public List<AccomodationPackage> AccomodationPackages { get; set; }
        public int? AccomodationPackagesId { get; set; }
        public Pager pager { get; set; }
        public string PictureIds { get; set; }
    }
}