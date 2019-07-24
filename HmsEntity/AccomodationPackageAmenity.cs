using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsEntity
{
   public class AccomodationPackageAmenity
    {
       public int Id { get; set; }

        public int AmenityId { get; set; }

        public virtual Amenity Amenity { get; set; }

        public int AccomodationPackageId { get; set; }
    }
}
