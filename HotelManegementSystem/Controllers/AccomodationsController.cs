using HmsServices;
using HotelManegementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManegementSystem.Controllers
{
    public class AccomodationsController : Controller
    {
        // GET: Accomoations
        public ActionResult Details( int AccomodationPackageId)
        {
            AccomodationPackagesViewModel model = new AccomodationPackagesViewModel();
            model.AccomodationPackage = AccomodationPackagesServices.Instance.GetByIds(AccomodationPackageId);
            var v = AccomodationPackagesServices.Instance.GetAllByAccomodationIds(model.AccomodationPackage.AccomodationTypeId);
            model.AccomodationPackages = v.OrderBy(x => x.Id).Take(3).ToList();
          
            return View("Index",model);
        }
    }
}