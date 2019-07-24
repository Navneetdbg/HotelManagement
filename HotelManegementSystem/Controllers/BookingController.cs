using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HmsEntity;
using HmsServices;
using HotelManegementSystem.ViewModels;
using Microsoft.AspNet.Identity;

namespace HotelManegementSystem.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index(int AccomodationPackageId)
        {
            BookingViewModels model = new BookingViewModels();
            model.Accomodations = AccomodationServices.Instance.GetAllByPackages(AccomodationPackageId);
            model.AccomodationPackage = AccomodationPackagesServices.Instance.GetByIds(AccomodationPackageId);

            return View(model);
        }

        public JsonResult Check(BookingViewModels models)
        {
            JsonResult json = new JsonResult();
            Booking booking = new Booking();
            var result = false;

            int t = DateTime.Compare(models.FromDate, DateTime.Now);
            int y = DateTime.Compare(models.Duration, models.FromDate);
            if (t > 0 && y > 0)
            {
                var accomodation = AccomodationServices.Instance.GetAllByPackages(models.AccomodationPackageId);
                var a = new Accomodation();
                List<Accomodation> accomodations = new List<Accomodation>();
                for (int i = 0; i < models.TotalRooms; i++)
                {
                    foreach (var item in accomodation)
                {
                    
                        a = BookingServices.Instance.Check(item.Id, models.Duration, models.FromDate);
                        if (a != null)
                        {
                            accomodations.Add(a);
                        }
                    }


                }
                if (accomodations == null || accomodations[0] == null || accomodations.Count() <= models.TotalRooms)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }

            }
            else
            {
                result = false;
            }
            if (result)
            {
                json.Data = new { Success = true, Message = "Room is Available" };
            }
            else
            {
                json.Data = new { Success = false, Message = "Room is not Available" };
            }
            return json;
        }

        [HttpPost]
        [Authorize(Roles = "Users")]
        public JsonResult Index(BookingViewModels models)
        {
            JsonResult json = new JsonResult();
            Booking booking = new Booking();

            var result = false;
            if (models.Id > 0)
            {


            }
            else
            {

                var accomodation = AccomodationServices.Instance.GetAllByPackages(models.AccomodationPackageId);
                // accomodation = accomodation.OrderBy(x => x.Id).Where(x => x.IsAvalable).Take(models.TotalRooms).ToList();
                var a = new Accomodation();
                List<Accomodation> accomodations = new List<Accomodation>();
                foreach (var item in accomodation)
                {
                    for (int i = 0; i < models.TotalRooms; i++)
                    {
                        a = BookingServices.Instance.Check(item.Id, models.Duration, models.FromDate);
                        if (a != null)
                        {
                            accomodations.Add(a);
                        }
                    }

                   
                }



                if (accomodations[0] == null || accomodations.Count() <= models.TotalRooms)
                {
                    result = false;
                }

                else
                {
                    if (accomodations != null)
                    {
                        accomodations = accomodation.OrderBy(x => x.Id).Take(models.TotalRooms).ToList();
                    }
                    else
                    {
                        accomodations = accomodations.OrderBy(x => x.Id).Take(models.TotalRooms).ToList();

                    }
                    booking.FromDate = models.FromDate;
                    booking.EndDate = models.Duration;
                    string userId = User.Identity.GetUserId();
                    booking.UserId = userId;
                    booking.BookingUsers = new List<BookingUsers>();
                    booking.BookingUsers.AddRange(accomodations.Select(x => new BookingUsers() { AccomodationId = x.Id }));
                    result = BookingServices.Instance.Save(booking);
                }




            }
            if (result)
            {
                json.Data = new { Success = true, Message = "Your Hotel Room is Booked" };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable To Book Hotel Room" };
            }
            return json;
        }
    }
}