using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class LocationViewModel :ViewModel
    {

        public string CreatedBy { get; set; }
        public int Skip { get; set; }

        public LocationSearchResult Location { get; set; }
    }

    public class LocationSearchResult
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Floor { get; set; }
        public Nullable<int> LocationTypeCD { get; set; }
        public int StatusCD { get; set; }
        public int TotalRow { get; set; }
        public string LocationTypeCDName { get; set; }
        public string StatusCDName { get; set; }

    }
}
