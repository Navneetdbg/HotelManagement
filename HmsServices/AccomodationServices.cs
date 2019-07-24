using HmsDatabase;
using HmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsServices
{
   public class AccomodationServices
    {
        #region Singleton Service
        public static AccomodationServices Instance
        {
            get
            {
                if (instance == null) instance = new AccomodationServices();
                return instance;
            }
        }

        private static AccomodationServices instance { get; set; }
        private AccomodationServices()
        {

        }



        #endregion

        public List<Accomodation> GetAllByPackages(int accomodationPackageId)
        {
            using (var db = new HmsContext())
            {



                return db.Accomodations.Where(x => x.AccomodationPackageId == accomodationPackageId).ToList();
            }

        }
        public Accomodation GetById(int Id)
        {
            using (var db = new HmsContext())
            {
                return db.Accomodations.Find(Id);
            }
        }
        public List<Accomodation> SearchAllAccomodation(string search, int? accomodationPackageId, int pageNo, int pageSize)
        {
            var db = new HmsContext();
            var accomodation = db.Accomodations.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                accomodation = accomodation.Where(a => a.Name.ToLower().Contains(search.ToLower()));
            }
            if (accomodationPackageId.HasValue && accomodationPackageId.Value > 0)
            {
                accomodation = accomodation.Where(a => a.AccomodationPackageId == accomodationPackageId.Value);
            }
            return accomodation.OrderBy(x => x.Name).Skip((pageNo - 1) * pageSize).
                        Take(pageSize).ToList();

        }

        public int SearchAllAccomodationPackagesCount(string search, int? accomodationPackageId)
        {
            var db = new HmsContext();
            var accomodation = db.Accomodations.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                accomodation = accomodation.Where(a => a.Name.ToLower().Contains(search.ToLower()));
            }
            if (accomodationPackageId.HasValue && accomodationPackageId.Value > 0)
            {
                accomodation = accomodation.Where(a => a.AccomodationPackageId == accomodationPackageId.Value);
            }
            return accomodation.Count();

        }

        public List<AccomodationImages> GetPicByAccomodationId(int Id)
        {
            var db = new HmsContext();
           var v= db.Accomodations.Find(Id).AccomodationImages.ToList();
            return v;
        }

        public bool Save(Accomodation model)
        {
            var db = new HmsContext();

            db.Accomodations.Add(model);
            return db.SaveChanges() > 0;

        }

        public bool Edit(Accomodation model)
        {
            var db = new HmsContext();
            var exist = db.Accomodations.Find(model.Id);
            db.AccomodationImages.RemoveRange(exist.AccomodationImages);

            db.AccomodationImages.AddRange(model.AccomodationImages);
            
            db.Entry(exist).CurrentValues.SetValues(model);
            return db.SaveChanges() > 0;


        }

        public List<Accomodation> GetAllaccomodations ()
        {
            var db = new HmsContext();
            return db.Accomodations.OrderBy(x=>x.Id).Take(9).ToList();
        }
        public bool Delete(Accomodation model)
        {
            using (var db = new HmsContext())
            {
                var data = db.Accomodations.Where(x => x.Id == model.Id).FirstOrDefault();
                db.AccomodationImages.RemoveRange(data.AccomodationImages);
                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                //db.AccomodationTypes.Remove(data);
                return db.SaveChanges() > 0;
            }


        }
    }
}
