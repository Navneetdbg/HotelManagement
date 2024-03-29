﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsEntity
{
   public class Accomodation
    {
        public int Id { get; set; }
        public int AccomodationPackageId { get; set; }
        public virtual AccomodationPackage AccomodationPackage { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsAvalable { get; set; }
        public virtual List<AccomodationImages> AccomodationImages { get; set; }
    }
}
