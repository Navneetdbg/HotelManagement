using HmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManegementSystem.Areas.DashBoard.ViewModel
{
    public class AmenityListingModel
    {
        public List<Amenity> Amenities { get; set; }

        public string search { get; set; }
    }

    public class AmenityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
}