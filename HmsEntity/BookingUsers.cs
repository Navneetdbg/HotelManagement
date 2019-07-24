using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsEntity
{
  public  class BookingUsers
    {
        public int Id { get; set; }

        public int AccomodationId { get; set; }

        public virtual Accomodation Accomodation { get; set; }
        public int BookingId { get; set; }
    }
}
