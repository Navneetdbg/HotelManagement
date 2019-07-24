using HmsDatabase;
using HmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsServices
{
   public class AmenitiesServices
    {
        #region Singleton Service
        public static AmenitiesServices Instance
        {
            get
            {
                if (instance == null) instance = new AmenitiesServices();
                return instance;
            }
        }

        private static AmenitiesServices instance { get; set; }
        private AmenitiesServices()
        {

        }

        #endregion


        public List<Amenity> SearchAllAmenities(string search)
        {
            var db = new HmsContext();
            var amenity = db.Amenities.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                amenity = amenity.Where(a => a.Name.ToLower().Contains(search.ToLower()));
            }
            return amenity.ToList();
        }

        public List<Amenity> getAmnetiesById()
        {
            throw new NotImplementedException();
        }

        public Amenity GetById(int Id)
        {
            var db = new HmsContext();
            return db.Amenities.Find(Id);
        }

        public bool Edit(Amenity model)
        {
            var db = new HmsContext();
            var data = GetById(model.Id);
            data.Name = model.Name;
            data.Description = model.Description;
            db.Entry(data).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool Save(Amenity model)
        {
            var db = new HmsContext();
            db.Amenities.Add(model);
            return db.SaveChanges() > 0;
        }

        public bool Delete(Amenity models)
        {
            using (var db = new HmsContext())
            {
                var data = db.Amenities.Where(x => x.Id == models.Id).FirstOrDefault();
                //db.AccomodationTypes.Attach(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                //db.AccomodationTypes.Remove(data);
                return db.SaveChanges() > 0;
            }
        }

        public List<AccomodationPackageAmenity> CurrentAmenities(int id)
        {
            var db = new HmsContext();
            return  db.AccomodationPackageAmenities.Where(x => x.AccomodationPackageId == id).ToList();
        }

        public bool AddAmenities(AccomodationPackageAmenity model)
        {
            var db = new HmsContext();
             db.AccomodationPackageAmenities.Add(model);
            return db.SaveChanges()> 0;
        }

        public bool DeleteAmenities(AccomodationPackageAmenity model)
        {
            var db = new HmsContext();
            var removed = db.AccomodationPackageAmenities.Where(x => x.AccomodationPackageId == model.AccomodationPackageId && x.AmenityId == model.AmenityId).FirstOrDefault();
            db.Entry(removed).State = System.Data.Entity.EntityState.Deleted;
            
            return db.SaveChanges() > 0;

        }

        //public List<Amenity> Contain(List<Amenity> amenities)
        //{
        //    var db = new HmsContext();
        //    var v = new List<AccomodationPackageAmenity>();
        //    foreach (var item in amenities)
        //    {
        //         v =  db.AccomodationPackageAmenities.Where(x => x.AmenityId != item.Id);
                
        //    }
        //    return v;
        //}
    }


}
