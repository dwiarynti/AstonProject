using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class MovementRequestDetailViewModel : ViewModel
    {
        public int ID { get; set; }
        public int MovementRequestID { get; set; }
        public string Description { get; set; }
        public int AssetCategoryCD { get; set; }
        public int Quantity { get; set; }
        public int RequestTo { get; set; }
      
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public int Transfered { get; set; }
    }
}
