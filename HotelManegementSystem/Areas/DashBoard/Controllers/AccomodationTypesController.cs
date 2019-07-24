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
    public class AccomodationTypesController : Controller
    {
        // GET: DashBoard/AccomodationTypes
        public ActionResult Index(string search)
        {
            AccomodationTypesListingModels models = new AccomodationTypesListingModels();
            models.Search = search;
            models.AccomodationTypes = AccomodationTypesServices.Instance.SearchAllAccomodationTypes(search);
            
            return View(models);
        }

     

        public ActionResult Action( int? Id)
        {
            AccomodationTypesActionModels models = new AccomodationTypesActionModels();
            if (Id == null)
            {
                
                return PartialView("_Action", models);
            }
            else
            {
                var accomodationType = AccomodationTypesServices.Instance.GetById(Id.Value);
                models.Id = accomodationType.Id;
                models.Name = accomodationType.Name;
                models.Description = accomodationType.Description;
                return PartialView("_Action", models);
            }
        }

        [HttpPost]
        public JsonResult Action(AccomodationType model)
        {
            JsonResult json = new JsonResult();
            var result = false;
            if(model.Id > 0)
            {
               
                result = AccomodationTypesServices.Instance.Edit(model);
            }
            else
            {
                 result = AccomodationTypesServices.Instance.Save(model);
            }
            
            if(result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false , Message = "Unable To Add Accomodation Type"};
            }
            return json;
           
            
        }

        public ActionResult Delete(int Id)
        {
            AccomodationTypesActionModels models = new AccomodationTypesActionModels();
            var accomodation = AccomodationTypesServices.Instance.GetById(Id);
            models.Id = accomodation.Id;
            return PartialView("Delete", models);

        }
        [HttpPost]
        public JsonResult Delete(AccomodationType models)
        {
            JsonResult json = new JsonResult();
            var result = false;
            var accomdationTypes = AccomodationTypesServices.Instance.GetById(models.Id);
            result = AccomodationTypesServices.Instance.Delete(models);
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