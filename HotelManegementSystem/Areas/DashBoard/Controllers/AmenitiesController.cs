using HmsEntity;
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
    public class AmenitiesController : Controller
    {
        // GET: DashBoard/Amenities
        public ActionResult Index(string search)
        {
            AmenityListingModel models = new AmenityListingModel();
            models.search = search;
            models.Amenities = AmenitiesServices.Instance.SearchAllAmenities(search);
            return View(models);
        }

        public ActionResult Action(int? Id)
        {
            AmenityViewModel models = new AmenityViewModel();
            if (Id == null)
            {

                return PartialView("_Action", models);
            }
            else
            {
                var accomodationType = AmenitiesServices.Instance.GetById(Id.Value);
                models.Id = accomodationType.Id;
                models.Name = accomodationType.Name;
                models.Description = accomodationType.Description;
                return PartialView("_Action", models);
            }
        }

        [HttpPost]
        public JsonResult Action(Amenity model)
        {
            JsonResult json = new JsonResult();
            var result = false;
            if (model.Id > 0)
            {

                result = AmenitiesServices.Instance.Edit(model);
            }
            else
            {
                result = AmenitiesServices.Instance.Save(model);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable To Add Accomodation Type" };
            }
            return json;


        }

        public ActionResult Delete(int Id)
        {
            AmenityViewModel models = new AmenityViewModel();
            var accomodation = AmenitiesServices.Instance.GetById(Id);
            models.Id = accomodation.Id;
            return PartialView("Delete", models);

        }
        [HttpPost]
        public JsonResult Delete(Amenity models)
        {
            JsonResult json = new JsonResult();
            var result = false;
            var accomdationTypes = AmenitiesServices.Instance.GetById(models.Id);
            result = AmenitiesServices.Instance.Delete(models);
            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable To Delete Accomodation Type" };
            }
            return json;

        }
    }
}