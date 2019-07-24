using HmsDatabase;
using HmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsServices
{
   public class DashboardServices
    {
        #region Singleton Service
        public static DashboardServices Instance
        {
            get
            {
                if (instance == null) instance = new DashboardServices();
                return instance;
            }
        }

        private static DashboardServices instance { get; set; }
        private DashboardServices()
        {

        }

        #endregion

        public bool savePicture(ImagesEntity model)
        {
            var db = new HmsContext();
            db.Images.Add(model);
            return db.SaveChanges() > 0;
        }

        public List<ImagesEntity> GetPicBtIds(List<int> picIds)
        {
            using (var db = new HmsContext())
            {
                return picIds.Select(x => db.Images.Find(x)).ToList();
            }
        }
    }
}
