using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class Location
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Floor { get; set; }
        public int LocationTypeCD { get; set; }
        public int StatusCD { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        
        public ICollection<AssetLocation> AssetLocation { get; set; }
        public ICollection<MovementRequest> MovementRequest { get; set; }
        public ICollection<AssetOpnameTransaction> AssetOpnameTransaction { get; set; }
    }
}
