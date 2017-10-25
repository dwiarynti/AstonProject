using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class AssetLocationViewModel : ViewModel
    {
        public int Skip { get; set; }
        
        public AssetLocationPagination AssetLocation { get; set; }
        public List<AssetLocation> AssetLocationList { get; set; }
    }

    public class AssetLocationPagination
    {
        public int ID { get; set; }
        public int AssetID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<bool> OnTransition { get; set; }
        public Nullable<int> MovementRequestDetailID { get; set; }
        public string LocationName { get; set; }
        public string AssetName { get; set; }
        public int TotalRow { get; set; }


    }
}
