using HmsDatabase;
using HmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsServices
{
    public class AccomodationTypesServices
    {
        #region Singleton Service
        public static AccomodationTypesServices Instance
        {
            get
            {
                if (instance == null) instance = new AccomodationTypesServices();
                return instance;
            }
        }

        private static AccomodationTypesServices instance { get; set; }
        private AccomodationTypesServices()
        {

        }

        #endregion
        public List<AccomodationType> GetAllAccomodationTypes()
        {
            var db = new HmsContext();
            return db.AccomodationTypes.ToList();
        }

        public List<AccomodationType> SearchAllAccomodationTypes(string search)
        {
            var db = new HmsContext();
            var accomodationType = db.AccomodationTypes.AsQueryable();
            if(!string.IsNullOrEmpty(search))
            {
                accomodationType= accomodationType.Where(a => a.Name.ToLower().Contains(search.ToLower()));
            }
            return accomodationType.ToList();
        }

        public bool Save(AccomodationType model)
        {
            var db = new HmsContext();
            db.AccomodationTypes.Add(model);
            return db.SaveChanges()> 0;
        }
        
        public AccomodationType GetById(int Id)
        {
            var db = new HmsContext();
          return  db.AccomodationTypes.Find(Id);
           
        }
        public bool Edit( AccomodationType model)
        {
            var db = new HmsContext();
            var data = GetById(model.Id);
            data.Name = model.Name;
            data.Description = model.Description;
            db.Entry(data).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges() > 0;

        }

        public bool Delete(AccomodationType models)
        {
            using (var db = new HmsContext())
            {
                var data = db.AccomodationTypes.Where(x=>x.Id == models.Id).FirstOrDefault();
                //db.AccomodationTypes.Attach(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                //db.AccomodationTypes.Remove(data);
                return db.SaveChanges() > 0;
            }
           

        }

    }
}
