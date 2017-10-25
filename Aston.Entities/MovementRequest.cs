using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class MovementRequest
    {
        public MovementRequest()
        {
            this.MovementRequestDetail = new HashSet<MovementRequestDetail>();
        }
        public int ID { get; set; }
        public string MovementDate { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string Description { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public int ApprovalStatus { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public virtual ICollection<MovementRequestDetail> MovementRequestDetail { get; set; }
        public virtual Location Location { get; set; }
    }
}
