using HmsServices;
using HotelManegementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManegementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IndexViewModels models = new IndexViewModels();
            models.AccomodationTypes = AccomodationTypesServices.Instance.GetAllAccomodationTypes();
            models.AccomodationPackages = AccomodationPackagesServices.Instance.GetAllAccomodationPackages();
            models.Amenities = AmenitiesServices.Instance.SearchAllAmenities("");
            return View(models);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}