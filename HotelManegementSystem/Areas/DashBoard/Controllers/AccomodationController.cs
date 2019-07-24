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
    public class AccomodationController : Controller
    {
        // GET: DashBoard/Accomodation
        public ActionResult Index(string search, int? AccomodationPackageId, int? pageNo)
        {
            AccomodationListingModels models = new AccomodationListingModels();
            int pageSize = 10;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            models.Search = search;
            models.AccomodationPackagesId = AccomodationPackageId;
            models.Accomodations = AccomodationServices.Instance.SearchAllAccomodation(search, AccomodationPackageId, pageNo.Value, pageSize);
            models.AccomodationPackages = AccomodationPackagesServices.Instance.GetAllAccomodationPackages();
            var totalRecords = AccomodationPackagesServices.Instance.SearchAllAccomodationPackagesCount(search, AccomodationPackageId);
            models.pager = new Pager(totalRecords, pageNo, pageSize);
            return View(models);
        }

        public ActionResult Action(int? Id)
        {
            AccomodationModels models = new AccomodationModels();
            Booking booking = new Booking();
            BookingUsers users = new BookingUsers();
            if (Id.HasValue)
            {
                var accomodationType = AccomodationServices.Instance.GetById(Id.Value);
                models.Id = accomodationType.Id;
                models.Name = accomodationType.Name;
                models.Description = accomodationType.Description;
                if(BookingServices.Instance.IsExist(models.Id))
                {
                    models.IsAvailable = false;
                }
                else
                {
                    models.IsAvailable = true;
                }
                models.AccomodationPackageId = accomodationType.AccomodationPackageId;
                models.AccomodationImages = AccomodationServices.Instance.GetPicByAccomodationId(accomodationType.Id);


            }
            models.AccomodationPackages = AccomodationPackagesServices.Instance.GetAllAccomodationPackages();
            return PartialView("_Action", models);
        }

        [HttpPost]
        public JsonResult Action(AccomodationModels models)
        {
            JsonResult json = new JsonResult();
            Accomodation model = new Accomodation();
            var result = false;
            List<int> picIds = !string.IsNullOrEmpty(models.PicTureIds)? models.PicTureIds.Split(',').Select(z => int.Parse(z)).ToList() : new List<int>();
            var pictures = DashboardServices.Instance.GetPicBtIds(picIds);
            if (models.Id > 0)
            {
                model.Id = models.Id;
                model.Name = models.Name;
                model.AccomodationPackageId = models.AccomodationPackageId;
                model.Description = models.Description;
                model.IsAvalable = models.IsAvailable;
                model.AccomodationImages = new List<AccomodationImages>();
                model.AccomodationImages.AddRange(pictures.Select(x => new AccomodationImages() { AccomodationId = model.Id ,ImageId = x.Id }));
                result = AccomodationServices.Instance.Edit(model);
            }
            else
            {
                model.Name = models.Name;
                model.AccomodationPackageId = models.AccomodationPackageId;
                model.Description = models.Description;
                model.IsAvalable = models.IsAvailable;
                model.AccomodationImages = new List<AccomodationImages>();
                model.AccomodationImages.AddRange(pictures.Select(x => new AccomodationImages() { ImageId = x.Id }));
                result = AccomodationServices.Instance.Save(model);
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
            AccomodationModels models = new AccomodationModels();
            var accomodationType = AccomodationServices.Instance.GetById(Id);
            models.Id = accomodationType.Id;
            return PartialView("_Delete", models);

        }

        [HttpPost]
        public ActionResult Delete(Accomodation models)
        {
            JsonResult json = new JsonResult();
            var result = false;
            var accomdationTypes = AccomodationServices.Instance.GetById(models.Id);
            result = AccomodationServices.Instance.Delete(models);
            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable To Delete Accomodation" };
            }
            return json;
        }

    }
}