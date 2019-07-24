using HmsDatabase;
using HmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsServices
{
    public class AccomodationPackagesServices
    {
        #region Singleton Service
        public static AccomodationPackagesServices Instance
        {
            get
            {
                if (instance == null) instance = new AccomodationPackagesServices();
                return instance;
            }
        }

        private static AccomodationPackagesServices instance { get; set; }

      

        private AccomodationPackagesServices()
        {

        }

        #endregion


        public List<AccomodationPackage> GetAllByAccomodationId(int accomdationTypeId)
        {
            using (var db = new HmsContext())
            {
                return db.AccomodationPackages.Where(x => x.AccomodationTypeId == accomdationTypeId).ToList();
            }
        }

        public List<AccomodationPackage> GetAllByAccomodationIds(int accomdationTypeId)
        {
            var db = new HmsContext();
            
            return db.AccomodationPackages.Where(x => x.AccomodationTypeId == accomdationTypeId).ToList();
            
        }
        public AccomodationPackage GetById(int Id)
        {
            using (var db = new HmsContext())
            {
                return db.AccomodationPackages.Find(Id);
            }
        }

        public AccomodationPackage GetByIds(int Id)
        {
            var db = new HmsContext();
            
                return db.AccomodationPackages.Find(Id);
            
        }
        public List<AccomodationPackage> GetAllAccomodationPackages()
        {
            var db = new HmsContext();
            return db.AccomodationPackages.ToList();
        }
        public List<AccomodationPackage> SearchAllAccomodationPackages(string search , int? accomodationTypeId , int pageNo , int pageSize)
        {
            var db = new HmsContext();
            var accomodationPackes = db.AccomodationPackages.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                accomodationPackes = accomodationPackes.Where(a => a.Name.ToLower().Contains(search.ToLower()));
            }
            if (accomodationTypeId.HasValue && accomodationTypeId.Value > 0)
            {
                accomodationPackes = accomodationPackes.Where(a => a.AccomodationTypeId == accomodationTypeId.Value);
            }
            return accomodationPackes.OrderBy(x=>x.Name).Skip((pageNo - 1) * pageSize).
                        Take(pageSize).ToList();

        }

        public List<AccomodationPackageImage> GetPicByAccomodationId(int Id)
        {
            var db = new HmsContext();
            return db.AccomodationPackages.Find(Id).AccomodationPackageImages.ToList();
        }

        public int SearchAllAccomodationPackagesCount(string search, int? accomodationTypeId)
        {
            var db = new HmsContext();
            var accomodationPackes = db.AccomodationPackages.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                accomodationPackes = accomodationPackes.Where(a => a.Name.ToLower().Contains(search.ToLower()));
            }
            if (accomodationTypeId.HasValue && accomodationTypeId.Value > 0)
            {
                accomodationPackes = accomodationPackes.Where(a => a.AccomodationTypeId == accomodationTypeId.Value);
            }
            return accomodationPackes.Count();

        }

        public bool Save(AccomodationPackage model)
        {
            var db = new HmsContext();

            db.AccomodationPackages.Add(model);
            return db.SaveChanges() > 0;

        }

        public bool Edit(AccomodationPackage model)
        {
            var db = new HmsContext();
            var exist = db.AccomodationPackages.Find(model.Id);
            db.AccomodationPackageImages.RemoveRange(exist.AccomodationPackageImages);
           
            db.AccomodationPackageImages.AddRange(model.AccomodationPackageImages);
           
            db.Entry(exist).CurrentValues.SetValues(model);
            return db.SaveChanges() > 0;


        }

        public bool Delete(AccomodationPackage model)
        {
            using (var db = new HmsContext())
            {
                var data = db.AccomodationPackages.Find(model.Id);
                db.AccomodationPackageImages.RemoveRange(data.AccomodationPackageImages);
                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                //db.AccomodationTypes.Remove(data);
                return db.SaveChanges() > 0;
            }


        }
    }
}
