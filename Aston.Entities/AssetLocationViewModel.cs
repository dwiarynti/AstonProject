using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class AssetLocationViewModel : ViewModel
    {
        public int ID { get; set; }
        public int AssetID { get; set; }
        public int LocationID { get; set; }
        public Nullable<bool> OnTransition { get; set; }
        public Nullable<int> MovementRequestDetailID { get; set; }
    }
}
