using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class MovementRequestDetail
    {
        public int ID { get; set; }
        public int MovementRequestID { get; set; }
        public string Description { get; set; }
        public int AssetCategoryCD { get; set; }
        public int Quantity { get; set; }
        public int RequestedTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public virtual MovementRequest MovementRequest { get; set; }
    }
}
