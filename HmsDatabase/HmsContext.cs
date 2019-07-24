using HmsEntity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsDatabase
{
    public class HmsContext : IdentityDbContext<HmsUser>
    {
        public HmsContext() : base("HmsDBs")
        {
        }

        public static HmsContext Create()
        {
            return new HmsContext();
        }
        public DbSet<AccomodationType> AccomodationTypes { get; set; }

        public DbSet<AccomodationPackage> AccomodationPackages { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<ImagesEntity> Images { get; set; }

        public DbSet<AccomodationImages> AccomodationImages { get; set; }

        public DbSet<AccomodationPackageImage> AccomodationPackageImages { get; set; }

        public DbSet<BookingUsers> BookingUsers { get; set; }

        public DbSet<Amenity> Amenities { get; set; }

        public DbSet<AccomodationPackageAmenity> AccomodationPackageAmenities { get; set; }

    }
}
