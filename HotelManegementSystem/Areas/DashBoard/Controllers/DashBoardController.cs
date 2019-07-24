using HmsEntity;
using HmsServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManegementSystem.Areas.DashBoard.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class DashBoardController : Controller
    {
        // GET: DashBoard/DashBoard
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Images()
        {
            JsonResult json = new JsonResult();

            var files = Request.Files;
            var ImageList = new List<ImagesEntity>();
            for (int i = 0; i < files.Count; i++)
            {
                var image = files[i];
                
                var path = Server.MapPath("~/Content/images/Site/");
                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var filePath = path + fileName;
                image.SaveAs(filePath);
                var dbImages = new ImagesEntity();
                dbImages.Url = fileName;
                if(DashboardServices.Instance.savePicture(dbImages))
                {
                    ImageList.Add(dbImages);
                    
                }
                
            }
            json.Data = ImageList;
            return json;
        }
    }
}