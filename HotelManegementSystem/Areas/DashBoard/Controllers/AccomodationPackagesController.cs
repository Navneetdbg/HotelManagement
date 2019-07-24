using HmsEntity;
using HmsServices;
using HotelManegementSystem.Areas.DashBoard.ViewModel;
using HotelManegementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManegementSystem.Areas.DashBoard.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class AccomodationPackagesController : Controller
    {
        // GET: DashBoard/AccomodationPackages
        public ActionResult Index( string search , int? AccomodationTypeId , int? pageNo)
        {
            AccomodationPackagesListing models = new AccomodationPackagesListing();
            int pageSize = 10;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            models.Search = search;
            models.AccomodationTypeId = AccomodationTypeId;
            models.AccomodationPackages = AccomodationPackagesServices.Instance.SearchAllAccomodationPackages(search, AccomodationTypeId, pageNo.Value, pageSize);
            models.AccomodationTypes = AccomodationTypesServices.Instance.GetAllAccomodationTypes();
            var totalRecords = AccomodationPackagesServices.Instance.SearchAllAccomodationPackagesCount(search, AccomodationTypeId);
            models.pager = new Pager(totalRecords,pageNo, pageSize);
            return View(models);
           
        }

        public ActionResult Action(int? Id)
        {
            AccomodationPackagesModels models = new AccomodationPackagesModels();
            if (Id.HasValue)
            {
                var accomodationType = AccomodationPackagesServices.Instance.GetById(Id.Value);
                models.Id = accomodationType.Id;
                models.Name = accomodationType.Name;
                models.NoOfRooms = accomodationType.NoOfRooms;
                models.PerNight = accomodationType.PerNight;
                models.AccomodationTypeId = accomodationType.AccomodationTypeId;
                models.Details = accomodationType.Details;
                models.AccomodationPackageImages = AccomodationPackagesServices.Instance.GetPicByAccomodationId(accomodationType.Id);

            }
            models.AccomodationTypes = AccomodationTypesServices.Instance.GetAllAccomodationTypes();
            return PartialView("_Action", models);
        }

        [HttpPost]
        public JsonResult Action(AccomodationPackagesModels model)
        {
            JsonResult json = new JsonResult();
            AccomodationPackage models = new AccomodationPackage();
           
                List<int> picIds = ! string.IsNullOrEmpty(model.PicTureIds)? model.PicTureIds.Split(',').Select(z => int.Parse(z)).ToList() : new List<int>();
                var pictures = DashboardServices.Instance.GetPicBtIds(picIds);
            
           
            var result = false;
            if (model.Id > 0)
            {
                models.Id = model.Id;
                models.Name = model.Name;
                models.NoOfRooms = model.NoOfRooms;
                models.PerNight = model.PerNight;
                models.AccomodationTypeId = model.AccomodationTypeId;
                models.Details = model.Details;
                models.AccomodationPackageImages = new List<AccomodationPackageImage>();
                models.AccomodationPackageImages.AddRange(pictures.Select(x => new AccomodationPackageImage() {AccomodationPackageId = models.Id , ImageId = x.Id }));

                result = AccomodationPackagesServices.Instance.Edit(models);
            }
            else
            {

                models.Name = model.Name;
                models.NoOfRooms = model.NoOfRooms;
                models.PerNight = model.PerNight;
                models.AccomodationTypeId = model.AccomodationTypeId;
                models.Details = model.Details;
                
                models.AccomodationPackageImages = new List<AccomodationPackageImage>();
                models.AccomodationPackageImages.AddRange(pictures.Select(x=> new AccomodationPackageImage() { ImageId = x.Id}));
                result = AccomodationPackagesServices.Instance.Save(models);
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
            AccomodationPackagesModels models = new AccomodationPackagesModels();
            var accomodationType = AccomodationPackagesServices.Instance.GetById(Id);
            models.Id = accomodationType.Id;
            return PartialView("_Delete", models);

        }

        [HttpPost]
        public ActionResult Delete(AccomodationPackage models)
        {
            JsonResult json = new JsonResult();
            var result = false;
            var accomdationTypes = AccomodationPackagesServices.Instance.GetById(models.Id);
            result = AccomodationPackagesServices.Instance.Delete(models);
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


        public ActionResult AddAmneties(int Id)
        {
            AccomodationPackageAmnetiesVM model = new AccomodationPackageAmnetiesVM();
            
            model.Id = Id;
            model.Amenities = AmenitiesServices.Instance.SearchAllAmenities("");
            model.CurrentAmenities = AmenitiesServices.Instance.CurrentAmenities(Id);
            //model.Amenities = AmenitiesServices.Instance.Contain(model.Amenities);
            return PartialView("_Amenities", model);
        }

        [HttpPost]
        public JsonResult AmenitiesAction(int AccomodationPackageId, int AmentiesId, bool isDelete = false)
        {
            JsonResult json = new JsonResult();
            AccomodationPackageAmenity model = new AccomodationPackageAmenity();
            model.AccomodationPackageId = AccomodationPackageId;
            model.AmenityId = AmentiesId;
            var result = false;
            if (isDelete == false)
            {

                result = AmenitiesServices.Instance.AddAmenities(model);
            }

            else
            {
                result = AmenitiesServices.Instance.DeleteAmenities(model);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable To do it Now" };
            }
            return json;
        }
    }
}