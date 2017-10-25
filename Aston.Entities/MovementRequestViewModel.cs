using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class MovementRequestViewModel : ViewModel
    { 

        //public int ID { get; set; }
        //public string MovementDate { get; set; }
        //public string Description { get; set; }
        //public string ApprovedDate { get; set; }
        //public string ApprovedBy { get; set; }
        //public int ApprovalStatus { get; set; }
        //public Nullable<int> LocationID { get; set; }
        //public string Notes { get; set; }
        public MovementRequestSearchResult MovementRequest { get; set; }
        public int Skip { get; set; }
      
        public string CreatedBy { get; set; }
      
        public string UpdatedBy { get; set; }
      
        public List<MovementRequestDetailViewModel> MovementRequestDetail { get; set; }
        //public int TotalRow { get; set; }

    }

    public class MovementRequestSearchResult
    {
        public int ID { get; set; }
        public string MovementDate { get; set; }
        public string Description { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<int> ApprovalStatus { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string Notes { get; set; }
        public string LocationName { get; set; }
        public string ApprovalStatusName { get; set; }
        public int TotalRow { get; set; }


    }
}
