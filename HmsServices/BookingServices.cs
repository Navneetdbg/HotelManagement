using HmsDatabase;
using HmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HmsServices
{
  public class BookingServices
    {
        #region Singleton Service
        public static BookingServices Instance
        {
            get
            {
                if (instance == null) instance = new BookingServices();
                return instance;
            }
        }

        private static BookingServices instance { get; set; }
        private BookingServices()
        {

        }

        #endregion

        public bool Save(Booking model)
        {
            var db = new HmsContext();
            db.Bookings.Add(model);

           return db.SaveChanges() > 0;
          
        }

        public bool SaveAccomodation(List<Accomodation> accomodations)
        {
            var db = new HmsContext();
            foreach (var item in accomodations)
            {
                var exist = db.Accomodations.Find(item.Id);

                item.IsAvalable = false;
                db.Entry(exist).CurrentValues.SetValues(item);
                
                
                //db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                
            }
            return db.SaveChanges() >0;
        }

        public bool IsExist(int AccomodationId)
        {
            var db = new HmsContext();
            try
            {
                var T = db.BookingUsers.Where(z => z.AccomodationId == AccomodationId).ToList();
                var v = db.BookingUsers.Where(z => z.AccomodationId == AccomodationId).OrderByDescending(x => x.Id).First();
                var Check = db.Bookings.Find(v.BookingId);
                if (DateTime.Now > Check.EndDate)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

       

        public Accomodation Check(int id, DateTime duration , DateTime fromDate)
        {
            using (var db = new HmsContext())
            {
                Accomodation accomodation = new Accomodation();
                var bookings = db.BookingUsers.Where(z => z.AccomodationId == id).ToList();
               
                    foreach (var item in bookings)
                    {
                        var book = db.Bookings.Find(item.BookingId);
                        if (book.FromDate == fromDate || book.EndDate == duration)
                        {
                            accomodation = null;
                        }
                        else
                        {
                            accomodation = item.Accomodation;
                        }


                    }
                
                return accomodation;
            }
        }

        public List<Booking> GetByUserId(string userId)
        {
            var db = new HmsContext();
            return db.Bookings.Where(x => x.UserId == userId).ToList();
        }

        public Booking GetBookingById( string userId, int Id)
        {
            var db = new HmsContext();
           var v = db.Bookings.Find(Id);
            if(v.UserId == userId)
            {
                return v;
            }
            else
            {
                return null;
            }

        }

        public List<Booking> Bookings()
        {
            var db = new HmsContext();
            return db.Bookings.OrderByDescending(x => x.Id).ToList();

        }
    }
}
