using HmsServices;
using HotelManegementSystem.Areas.DashBoard.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManegementSystem.Areas.DashBoard.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class BookingsController : Controller
    {
        // GET: DashBoard/Booking
        public ActionResult Index(string search)
        {
            BookingListingVM model = new BookingListingVM();
            model.Search = search;
            
            model.AccomodationPackages = AccomodationPackagesServices.Instance.GetAllAccomodationPackages();
            model.Bookings = BookingServices.Instance.Bookings();
            
            return View(model);
        }
    }
}