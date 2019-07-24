using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsEntity
{
  public  class AccomodationPackage
    {
        public int Id { get; set; }
        public int AccomodationTypeId { get; set; }

        public virtual AccomodationType AccomodationType { get; set; }
        public string Name { get; set; }
        public int NoOfRooms { get; set; }

        public string Details { get; set; }

        public virtual List<AccomodationPackageImage> AccomodationPackageImages { get; set; }

        public virtual List<AccomodationPackageAmenity> AccomodationPackageAmenities { get; set; }
        public decimal PerNight { get; set; }
    }
}
