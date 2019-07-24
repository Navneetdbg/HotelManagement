using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsEntity
{
   public class AccomodationImages
    {
        public int Id { get; set; }

        public int ImageId { get; set; }

        public virtual ImagesEntity Image { get; set; }

        public int AccomodationId { get; set; }
    }
}
