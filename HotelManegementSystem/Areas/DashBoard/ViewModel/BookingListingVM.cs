using HmsEntity;
using HotelManegementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManegementSystem.Areas.DashBoard.ViewModel
{
    public class BookingListingVM
    {
        public List <Booking> Bookings { get; set; }

        public List<AccomodationPackage> AccomodationPackages { get; set; }

        public string Search { get; set; }

        public Pager Pager { get; set; }

    }
}